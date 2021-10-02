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
		Angry
	}

	public enum Activity
	{
		WalkingRight,
		WalkingLeft,
		ExecutingAction
	}

	public float speed;
	public Rigidbody2D rb;
	public PandaMode mode;
	public Activity activity;

	// Start is called before the first frame update
	void Start()
	{
		activity = Activity.WalkingRight;
		mode = PandaMode.Regular;
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
	}

	void walk(int d)
	{
		rb.AddForce(Vector2.right*speed*d);
	}

	private void SetCorrectWalkDirection()
	{
		bool rightObstructed = Obstructed(rb.position, Vector2.right);
		bool leftObstructed = Obstructed(rb.position, Vector2.left);

		if (activity == Activity.WalkingRight && rightObstructed)
		{
			activity = Activity.WalkingLeft;
			return;
		}

		if (activity == Activity.WalkingLeft && leftObstructed)
		{
			activity = Activity.WalkingRight;
			return;
		}
	}

	private bool Obstructed(Vector2 position, Vector2 direction)
	{
		RaycastHit2D raycastHit2d_top =    Physics2D.Raycast(position + direction*0.5f + Vector2.up*0.5f, direction);
		RaycastHit2D raycastHit2d_bottom = Physics2D.Raycast(position + direction*0.5f - Vector2.up*0.5f, direction);

		return
			(raycastHit2d_top.transform != null && raycastHit2d_top.distance < 0.3)
			||
			(raycastHit2d_bottom.transform != null && raycastHit2d_bottom.distance < 0.3);
	}
}
