using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovment : MonoBehaviour
{
    public bool grounded; 

   private Rigidbody2D rb;

    private BoxCollider2D coll;

    private SpriteRenderer sprite;

    private Animator anim;

    private float dirX = 0f
        ;
    [SerializeField] private LayerMask jumpableGround;



   [SerializeField]private float moveSpeed = 7f; 
   [SerializeField] private float jumpForce = 12f;

    private enum MovementState { idle, runing, jumping, fall }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
       coll = GetComponent<BoxCollider2D>();

    }
    

    // Update is called once per frame
    private void Update()
    {
         dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed,rb.velocity.y);

        if (Input.GetButtonDown("Jump") &&IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        UpdateAnimationState();
        
    }

    private void UpdateAnimationState()
    {
        MovementState state;   


        if (dirX > 0f)
        {
            state = MovementState.runing;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.runing;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
            
        }

        if(rb.velocity.y > 1f)
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("state", (int)state);


    }
    private bool IsGrounded => Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

}

