using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePandaBehavior : MonoBehaviour
{

    public int speed = 1;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = 0.1f;
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }
}
