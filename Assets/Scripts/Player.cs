using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private Rigidbody2D rb;
    private bool faceRight = false;

    [SerializeField] float speed = 8f;
    [SerializeField] MainSM mainSM;

    GameObject currentObject;
    bool objectTrigger = false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if(objectTrigger && Input.GetKeyDown(KeyCode.E))
        {
            currentObject.GetComponent<Interactable>().Interact();
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "CameraMove") mainSM.CameraFollow();
        if(collision.tag == "Object")
        {
            Debug.Log("Enter");
            objectTrigger = true;
            currentObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraMove") mainSM.CameraUnfollow();
        if (collision.tag == "Object")
        {
            objectTrigger = false;
            currentObject = null;
        }
    }

}
