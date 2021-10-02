using UnityEngine;

public class WalkingController : MonoBehaviour
{
	public float speed;
	public Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		//rb.velocity = new Vector2(speed, rb.velocity.y);
		rb.AddForce(Vector2.right*speed);
	}
}
