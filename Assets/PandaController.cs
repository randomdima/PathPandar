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
	//public Collider2D rightCollider;
	//public Collider2D leftCollider;

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

			//Debug.Log($"r{Obstructed(rb.position+new Vector2(0.7f, 0), Vector2.right)} l{Obstructed(rb.position-new Vector2(0.7f, 0), Vector2.left)}");

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
/*
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
*/

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
		//string log = "";

		//var results = new List<RaycastHit2D>();
		//Physics2D.Linecast(position, position + direction, , results);

		
		RaycastHit2D raycastHit2d_top =    Physics2D.Raycast(position + direction*0.5f + Vector2.up*0.5f, direction);
		RaycastHit2D raycastHit2d_bottom = Physics2D.Raycast(position + direction*0.5f - Vector2.up*0.5f, direction);
		//log += $" {raycastHit2d == null}";
		/*
		log += $" {raycastHit2d.distance}";

		var renderer = gameObject.GetComponent<SpriteRenderer>();//(typeof(SpriteRenderer));
		if (renderer != null)
		{
			log += $" renderer found";
			renderer.sprite.texture.SetPixel(0, 0, Color.red);
			renderer.sprite.texture.SetPixel(10, 10, Color.red);
			renderer.sprite.texture.SetPixel(0, 10, Color.red);
			renderer.sprite.texture.SetPixel(10, 0, Color.red);

			renderer.sprite.texture.SetPixel(10, -10, Color.red);
			renderer.sprite.texture.SetPixel(0, -10, Color.red);
		}
		*/

		//Debug.Log($"{direction.x} t{raycastHit2d_top.distance} rb{raycastHit2d_top.transform!=null} b{raycastHit2d_bottom.distance}");
		return
			(raycastHit2d_top.transform != null && raycastHit2d_top.distance < 0.3)
			||
			(raycastHit2d_bottom.transform != null && raycastHit2d_bottom.distance < 0.3);
	}

	private void OnCollisionEnter(Collision collision)
	{

		Debug.Log("asdf");

	}
}
