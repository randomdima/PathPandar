using System;
using System.Collections;
using System.Collections.Generic;
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
	public Collider2D rightCollider;
	public Collider2D leftCollider;

	// Start is called before the first frame update
	void Start()
	{
		activity = Activity.WalkingRight;
		mode = PandaMode.Regular;
	}

	// Update is called once per frame
	void Update()
	{
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

	void walk(int d)
	{
		rb.AddForce(Vector2.right*speed*d);
	}

	private void OnTriggerEnter(Collider collider)
	{
		//Console.WriteLine("asdf");
		Debug.Log("asdf");

		if (activity == Activity.ExecutingAction)
		{
			return;
		}

		if (collider == rightCollider)
		{
			activity = Activity.WalkingLeft;
		}

		if (collider == leftCollider)
		{
			activity = Activity.WalkingRight;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{

		Debug.Log("asdf");

	}
}
