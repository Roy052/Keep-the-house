using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private Rigidbody2D rb;
    private bool faceRight = false;
    public Animator animator;

    [SerializeField] float speed = 8f;
    [SerializeField] MainSM mainSM;

    public GameObject currentObject;
    public bool objectTrigger = false;
    int objectType = -1; //0 : Closet, 1 : Desk, 2 : Door, 3 : Lightswitch

    public bool moveStop;

    AudioSource audioSource;
    bool isWalking = false;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        moveStop = true;
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("MoveSpeed", Mathf.Abs( horizontal));
        if(isWalking == false && Mathf.Abs(horizontal) >= 0)
        {
            isWalking = true;
            audioSource.Play();
        }
        else if(isWalking == true && Mathf.Abs(horizontal) == 0)
        {
            isWalking = false;
            audioSource.Stop();
        }
        if(objectTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if(objectType == 0)
            {
                if (currentObject.GetComponent<Closet>().objectON)
                    currentObject.GetComponent<Closet>().ClosetClose();
                else
                    currentObject.GetComponent<Closet>().ClosetOpen();
            }
            else if(objectType == 1)
            {
                currentObject.GetComponent<Desk>().CheckEnd();
            }
            else if (objectType == 2)
            {
                if (currentObject.GetComponent<Door>().objectON)
                    currentObject.GetComponent<Door>().DoorClose();
                else
                    currentObject.GetComponent<Door>().DoorOpen();
            }
            else if (objectType == 3)
            {
                if (currentObject.GetComponent<Lightswitch>().objectON)
                    currentObject.GetComponent<Lightswitch>().LightOFF();
                else
                    currentObject.GetComponent<Lightswitch>().LightON();
            }
            
        }
    }

    private void FixedUpdate()
    {
        if(moveStop == false)
        {
            
            rb.velocity = new Vector2(horizontal * speed, 0);
            Flip();
            
        }    
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
        if (collision.tag == "CameraMove") 
            mainSM.CameraFollow();

        if(collision.tag == "Closet")
        {
            Debug.Log("Enter");
            objectTrigger = true;
            objectType = 0;
            currentObject = collision.gameObject;
        }
        if(collision.tag == "Desk")
        {
            objectTrigger = true;
            objectType = 1;
            currentObject = collision.gameObject;
        }
        if(collision.tag == "Door")
        {
            objectTrigger = true;
            objectType = 2;
            currentObject = collision.gameObject;
        }
        if(collision.tag == "Lightswitch")
        {
            objectTrigger = true;
            objectType = 3;
            currentObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CameraMove") mainSM.CameraUnfollow();
        else
        {
            //Different Object
            if (currentObject == collision.gameObject)
            {
                objectTrigger = false;
                objectType = -1;
                currentObject = null;
            }
        }
    }

}
