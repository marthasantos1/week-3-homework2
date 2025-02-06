
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

    bool isFacingRight = true;

    //coin manager
    public CoinManager cm;
   



    Animator anim;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>(); 
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);

        if ((isFacingRight && direction == -1) || (!isFacingRight && direction == 1 ))
            Flip();

     
    }

    void OnMove(InputValue value)
    {
        float v = value.Get<float>();
        direction = v;
        
    }

    void Move(float dir)
    {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        anim.SetBool("isRunning", dir !=0);
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


 

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1f;
        transform.localScale = newLocalScale;

    }

   void OnTriggerEnter2D(Collider2D other) 
   {
        if (other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
        }
    
   }
}


