using UnityEngine;
using System;


public class PlayerJump : MonoBehaviour
{
    private Rigidbody2D rb; 

    [Header("Jump Settings")]
    public float jumpPower = 10f; // How strong the jump is

    [Header("Ground Check")]
    public Transform groundCheck; // Empty object below player feet
    public LayerMask groundLayer; // Layer assigned to ground objects
    public float groundCheckRadius = 0.15f;

    private bool isGrounded;
    private bool canJump = true;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Check and update velocities for animation transitions ---
        animator.SetFloat("xVelocity", Math.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);

        // --- Check if the player is touching the ground ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Reset jump lock when player is grounded ---
        if (isGrounded)
        {
            canJump = true;
        }

        // --- Jump input and lock ---
        if (Input.GetButtonDown("Jump") && isGrounded && canJump)
        {
            Jump();
            audioManager.PlaySFX(audioManager.jump);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    void Jump()
    {
        // Apply upward velocity and lock the jump
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        canJump = false;
    }

    // --- Optional: visualize ground check in editor ---
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }
}
