
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    Rigidbody2D rb;

    //dash code 

    //new code elements
    bool doubleJump;

    [SerializeField] float speed = 8f;
    [SerializeField] float jumpHeight = 3f;
    

    
    float direction = 0;
   


    bool isGrounded = false;
   

    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);

     
    }

    void OnMove(InputValue value)
    {
        float v = value.Get<float>();
        direction = v;
        
    }

    void Move(float dir)
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
    }

    void OnJump()
    {
        if (isGrounded)
        {
            Jump();
            doubleJump = true;
        }
        else if (doubleJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
            doubleJump = false;
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (Vector2.Angle(collision.GetContact(i).normal, Vector2.up) < 45f)
                {
                    isGrounded = true;
                }

            }

        }      
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

    }


// start of new code 


   
}


