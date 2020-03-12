using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D myRigid;
    private Animator myAnimator;
    [SerializeField] private float movementSpeed;
    private bool facigRight;
    [SerializeField] private Transform[] groundCheck;
    [SerializeField] private float grondRadius;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool jump;
    [SerializeField] private bool airControl;
    [SerializeField] private float jumpForce;    

    void Start()
    {
        facigRight = true;
        myRigid = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        HandleInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        //HandleLayers(); // line2 2
        ResetValues();
    }

    private void HandleMovement(float horizontal)
    {
         myRigid.velocity = new Vector2(horizontal * movementSpeed, myRigid.velocity.y);
            myAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if (myRigid.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (isGrounded || airControl)
        {
           
        }
       
        if (isGrounded && jump)
        {
            isGrounded = false;
            myRigid.AddForce(new Vector2(0, jumpForce));
            //myAnimator.SetTrigger("jump");  // line 3
        }

    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }
    private void Flip (float horizontal)
    {
        if (horizontal > 0 && !facigRight || horizontal< 0 && facigRight)
        {
            facigRight = !facigRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    private void ResetValues()
    {
        jump = false;
    }
    private bool IsGrounded()
    {
        if (myRigid.velocity.y <= 0)
        {
            foreach ( Transform point in groundCheck)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, grondRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        //myAnimator.ResetTrigger("jump");  // Line 4
                        //myAnimator.SetBool("land", false);  // line 5
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //private void HandleLayers()                 //all the lines 1
    //{
    //    if (!isGrounded)
    //    {
    //        myAnimator.SetLayerWeight(1, 1);
    //    }
    //    else
    //    {
    //        myAnimator.SetLayerWeight(1, 0);
    //    }

    //}
}
