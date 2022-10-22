using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float horizontal;
    private Rigidbody2D rb;
    private bool faceRight = false;

    [SerializeField] float speed = 8f;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, 0);
        Flip();
    }

    private void Flip()
    {
        if((faceRight == true && horizontal < 0f) || (faceRight == false && horizontal > 0f))
        {
            faceRight = !faceRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
