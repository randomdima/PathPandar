using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DTerrain;
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

	public Animator animator;
	public HungerController hungerController;

	public BlockController blockController;
	public GameObject bombPrefab;
	
	public float speed;
	public Rigidbody2D rb;
	public PandaMode mode;
	public Activity activity;


	public float offsetBottom = 0.5f;
	public float offsetTop = 0.5f;
	public float offsetSide = 0.5f;
	public float rayColisionDistance = 0.3f;

	public float bombPlacementPeriod = 10f;
	public float bombCountdown = 0f;

	public void MakeAngry()
	{
		mode = PandaMode.Angry;
		bombCountdown = bombPlacementPeriod;
	}

	// Start is called before the first frame update
	void Start()
	{
		activity = Activity.WalkingRight;
		mode = (PandaMode) (new Random().Next() % 4);
	}

	private float obscuredTime = 0f;

	private bool triedToJump = false;
	// Update is called once per frame
	void Update()
	{
		if (activity == Activity.WalkingLeft || activity == Activity.WalkingRight)
		{
			var leftColliders = GetSideCollisions(Vector2.left);
			var rightColliders = GetSideCollisions(Vector2.right);
			//Debug.Log(rightColliders.First().name);

			switch (mode)
			{
				case PandaMode.Angry:
					bombCountdown -= Time.deltaTime;
					//if (bombCounterStartTime + bombPlacementThreshold < Time.time)
					if (bombCountdown < 0)
					{
						//Debug.Log("place bomb");
						var bomb = Instantiate(bombPrefab, rb.transform.position, Quaternion.identity, GameObject.Find("BlockController").transform);
						bombCountdown = bombPlacementPeriod;
					}
					break;
				case PandaMode.Chef:
					rightColliders.Union(leftColliders).ToList().ForEach(q =>
					{
						var panda = q.gameObject.GetComponent<PandaController>();
						if (panda != null && panda.mode == PandaMode.Angry)
						{
							panda.hungerController.angerLevel = 0;
							panda.mode = PandaMode.Regular;
						}
					});
					break;
				case PandaMode.Builder:
					break;
				case PandaMode.Digger:
					break;
			}

			var isObscured = Obstructed(leftColliders);
			if (isObscured)
			{
				if (obscuredTime == 0f)
					obscuredTime = Time.time;

				if (Time.time - obscuredTime > 2f)
				{
					obscuredTime = 0;
					ChangeDirection();
					triedToJump = false;
				}
				else if (Time.time - obscuredTime > 0.3f && !triedToJump)
				{
					Jump();
					triedToJump = true;
				}
			}
			else
			{
				obscuredTime = 0;
				triedToJump = false;
			}

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

	void Jump()
	{
		rb.velocity += new Vector2(0, 3f*(0.3f+UnityEngine.Random.value));
	}

	private void ChangeDirection()
	{
		if (activity == Activity.WalkingRight)
			activity = Activity.WalkingLeft;
		else
			activity = Activity.WalkingRight;
		Flip();
	}

	private void SetCorrectWalkDirection(bool leftObstructed, bool rightObstructed)
	{
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

	private bool Obstructed(List<Collider2D> collisionList)
	{
		return collisionList.Any(c => c.transform!=null && !c.isTrigger);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    private List<Collider2D> GetSideCollisions(Vector2 direction)
	{
        var list = new List<Collider2D>();
        GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), list);

        //change Raycast to collider
        /*RaycastHit2D raycastHit2d_top =    Physics2D.Raycast(rb.position + direction*offsetSide + Vector2.up*offsetTop,    direction);
		RaycastHit2D raycastHit2d_bottom = Physics2D.Raycast(rb.position + direction*offsetSide - Vector2.up*offsetBottom, direction);

		var list = new List<Collider2D>();
		if (raycastHit2d_top.transform != null && raycastHit2d_top.distance < rayColisionDistance)
		{
			list.Add(raycastHit2d_top.collider);
		}

		if (raycastHit2d_bottom.transform != null && raycastHit2d_bottom.distance < rayColisionDistance)
		{
			list.Add(raycastHit2d_bottom.collider);
		}*/

		return list;
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
