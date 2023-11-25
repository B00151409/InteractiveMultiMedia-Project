using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpHeight = 14f;
    public float deathLimit = -5.0f;

    private Rigidbody2D rb;
    private float horizontalInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;
    private bool isGrounded;
    private int jumpsLeft = 2; 

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;

    //define the animation options in an enum
    private enum AnimationState { idle, running, jumping, falling }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        
        isGrounded = groundDetection();

        if (isGrounded)
        {
            
            jumpsLeft = 2;
        }

        if (Input.GetKeyDown("space") && jumpsLeft > 0)
        {
            jumpSound.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            jumpsLeft--;
        }

        AnimationUpdate();
    }




    private void AnimationUpdate()
    {
        AnimationState state;

        if (horizontalInput > 0f)
        {
            state = AnimationState.running;
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0f)
        {
            state = AnimationState.running;
            spriteRenderer.flipX = true;
        }
        else
        {
            state = AnimationState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = AnimationState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = AnimationState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsLeft = 2; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private bool groundDetection()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, layerMask);
    }

    public void Die()
    {
        animator.SetTrigger("death");
        rb.bodyType = RigidbodyType2D.Static;
        deathSound.Play();
        
    }

    private void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
