using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

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

	public void MakeAngry()
	{
		mode = PandaMode.Angry;
	}

	// Start is called before the first frame update
	void Start()
	{
		activity = Activity.WalkingRight;
		mode = (PandaMode) (new Random().Next() % 4);
	}

	// Update is called once per frame
	void Update()
	{
		if (activity == Activity.WalkingLeft || activity == Activity.WalkingRight)
		{
			var leftColliders = GetSideCollisions(Vector2.left);
			var rightColliders = GetSideCollisions(Vector2.right);
			//Debug.Log(rightColliders.First().name);

			SetCorrectWalkDirection(Obstructed(leftColliders), Obstructed(rightColliders));


			switch (activity)
			{
				case Activity.WalkingRight:
					walk(1);
					break;
				case Activity.WalkingLeft:
					walk(-1);
					break;
				case Activity.ExecutingAction:
					break;
			}
		}

		SetAnimatorActivitiAndMode();
	}

	void walk(int d)
	{
		Vector3 targetVelocity = new Vector2(speed * d, rb.velocity.y);
		// And then smoothing it out and applying it to the character
		rb.velocity = targetVelocity;
		//rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref curVelocity, 0.05f);
	}

	private void SetCorrectWalkDirection(bool leftObstructed, bool rightObstructed)
	{
		//bool rightObstructed = Obstructed(rb.position, Vector2.right);
		//bool leftObstructed = Obstructed(rb.position, Vector2.left);

		//Debug.Log($"{leftObstructed} {rightObstructed}");


		if (activity == Activity.WalkingRight && rightObstructed)
		{
			activity = Activity.WalkingLeft;
			Flip();
			return;
		}

		if (activity == Activity.WalkingLeft && leftObstructed)
		{
			activity = Activity.WalkingRight;
			Flip();
			return;
		}
	}

	private void Flip()
	{
		Vector3 scale = rb.transform.localScale;
		var dir = activity == Activity.WalkingLeft ? -1 : 1;
		scale.x = Math.Abs(scale.x)*dir;
		rb.transform.localScale = scale;
	}

	private bool Obstructed(Vector2 position, Vector2 direction)
	{
		RaycastHit2D raycastHit2d_top =    Physics2D.Raycast(position + direction*offsetSide + Vector2.up*offsetTop,    direction);
		RaycastHit2D raycastHit2d_bottom = Physics2D.Raycast(position + direction*offsetSide - Vector2.up*offsetBottom, direction);

		bool topCollision = raycastHit2d_top.transform != null && raycastHit2d_top.distance < rayColisionDistance;
		bool bottomCollistion = raycastHit2d_bottom.transform != null && raycastHit2d_bottom.distance < rayColisionDistance;

		return
			(topCollision && !raycastHit2d_top.collider.isTrigger)
			||
			(bottomCollistion && !raycastHit2d_bottom.collider.isTrigger);
	}

	private bool Obstructed(List<Collider2D> collisionList)
	{
		return collisionList.Any(c => c.transform!=null && !c.isTrigger);
	}

	private List<Collider2D> GetSideCollisions(Vector2 direction)
	{
		RaycastHit2D raycastHit2d_top =    Physics2D.Raycast(rb.position + direction*offsetSide + Vector2.up*offsetTop,    direction);
		RaycastHit2D raycastHit2d_bottom = Physics2D.Raycast(rb.position + direction*offsetSide - Vector2.up*offsetBottom, direction);

		var list = new List<Collider2D>();
		if (raycastHit2d_top.transform != null && raycastHit2d_top.distance < rayColisionDistance)
		{
			list.Add(raycastHit2d_top.collider);
		}

		if (raycastHit2d_bottom.transform != null && raycastHit2d_bottom.distance < rayColisionDistance)
		{
			list.Add(raycastHit2d_bottom.collider);
		}

		return list;
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

		var walking = activity == Activity.WalkingLeft || activity == Activity.WalkingRight;
		setAnimatorWalking(walking);

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
				if (walking)
				{
					setAnimatorDigger(true);
				}
				else
				{
					setAnimatorDiggerDigging(true);
				}
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
		setAnimatorDiggerDigging(false);
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

	private void setAnimatorDiggerDigging(bool value)
	{
		animator.SetBool("is_digger_digging", value);
	}
}
