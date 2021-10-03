using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PandaController : MonoBehaviour
{
	public enum PandaMode
	{
		Regular,
		Builder,
		Chef,
		Digger,
		Angry
	}

	public enum Activity
	{
		WalkingRight,
		WalkingLeft,
		ExecutingAction
	}

	public Transform topRightCheck;
	public Transform bottomRightCheck;
	public Transform topLeftCheck;
	public Transform bottomLeftCheck;
	public Transform ceilingCheck;
	public Transform groundCheck;
	public LayerMask m_whatIsGround;

	public Animator animator;
	
	public bool facingRight;
	public float speed;
	public Rigidbody2D rb;
	public PandaMode mode;
	public Activity activity;


	public float offsetBottom = 0.5f;
	public float offsetTop = 0.5f;
	public float offsetSide = 0.5f;
	public float rayColisionDistance = 0.3f;

	// Start is called before the first frame update
	void Start()
	{
		activity = Activity.WalkingRight;
		mode = PandaMode.Regular;
		//walk(1);
	}

	// Update is called once per frame
	void Update()
	{
		if (activity == Activity.WalkingLeft || activity == Activity.WalkingRight)
		{
			SetCorrectWalkDirection();

			if (activity == Activity.WalkingRight)
			{
				walk(1);
			}
			else
			{
				walk(-1);
			}
		}

		SetAnimatorActivitiAndMode();
	}

	void walk(int d)
	{
		//rb.AddForce(Vector2.right*speed*d);

		Vector3 targetVelocity = new Vector2(speed * d, rb.velocity.y);
		// And then smoothing it out and applying it to the character
		Vector3 curVelocity;
		rb.velocity = targetVelocity;
		//rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref curVelocity, 0.05f);
	}

	private void SetCorrectWalkDirection()
	{
		//bool rightObstructed = ObstructedRight();
		//bool leftObstructed = ObstructedLeft();

		bool rightObstructed = Obstructed(rb.position, Vector2.right);
		bool leftObstructed = Obstructed(rb.position, Vector2.left);

		if (activity == Activity.WalkingRight && rightObstructed)
		{
			Flip();
			activity = Activity.WalkingLeft;
			return;
		}

		if (activity == Activity.WalkingLeft && leftObstructed)
		{
			Flip();
			activity = Activity.WalkingRight;
			return;
		}
	}

	private void Flip()
	{
		Vector3 scale = rb.transform.localScale;
		scale.x *= -1;
		rb.transform.localScale = scale;
	}

	private bool Obstructed(Vector2 position, Vector2 direction)
	{

		RaycastHit2D raycastHit2d_top =    Physics2D.Raycast(position + direction*offsetSide + Vector2.up*offsetTop,    direction);
		RaycastHit2D raycastHit2d_bottom = Physics2D.Raycast(position + direction*offsetSide - Vector2.up*offsetBottom, direction);

		return
			(raycastHit2d_top.transform != null && raycastHit2d_top.distance < rayColisionDistance)
			||
			(raycastHit2d_bottom.transform != null && raycastHit2d_bottom.distance < rayColisionDistance);
	}

	private bool ObstructedRight()
	{
		float radius = 0.1f;
		return
			Physics2D.OverlapCircle(topRightCheck.position, radius, m_whatIsGround)
			||
			Physics2D.OverlapCircle(bottomRightCheck.position, radius, m_whatIsGround);
	}

	private bool ObstructedLeft()
	{
		float radius = 0.1f;
		return
			Physics2D.OverlapCircle(topLeftCheck.position, radius, m_whatIsGround)
			||
			Physics2D.OverlapCircle(bottomLeftCheck.position, radius, m_whatIsGround);
	}

	private void SetAnimatorActivitiAndMode()
	{
		ResetAnimaator();

		setAnimatorWalking(activity == Activity.WalkingLeft || activity == Activity.WalkingRight);

		switch (mode)
		{
			case PandaMode.Angry:
				setAnimatorAngry(true);
				break;
			case PandaMode.Builder:
				setAnimatorBuilder(true);
				break;
			case PandaMode.Chef:
				setAnimatorChef(true);
				break;
			case PandaMode.Regular:
				setAnimatorRegular(true);
				break;
			case PandaMode.Digger:
				setAnimatorDigger(true);
				break;
		}
	}

	private void ResetAnimaator()
	{
		setAnimatorAngry(false);
		setAnimatorBuilder(false);
		setAnimatorChef(false);
		setAnimatorDigger(false);
		setAnimatorRegular(false);
		setAnimatorWalking(false);
	}

	private void setAnimatorAngry(bool value)
	{
		animator.SetBool("is_angry", value);
	}

	private void setAnimatorDigger(bool value)
	{
		animator.SetBool("is_digger", value);
	}
	private void setAnimatorChef(bool value)
	{
		animator.SetBool("is_chef", value);
	}
	private void setAnimatorBuilder(bool value)
	{
		animator.SetBool("is_builder", value);
	}

	private void setAnimatorRegular(bool value)
	{
		animator.SetBool("is_regular", value);
	}

	private void setAnimatorWalking(bool value)
	{
		animator.SetBool("is_walking", value);
	}
}
