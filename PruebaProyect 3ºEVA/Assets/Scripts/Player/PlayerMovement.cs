using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Salto")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    public bool isGrounded;
    private float moveInput = 0f;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        HandleFlip();
        UpdateAnimations();
    }

    void UpdateAnimations()
    {
        animator.SetBool("isRunning", moveInput != 0 && isGrounded);
        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0);
        animator.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < 0);

        Debug.Log("isGrounded: " + isGrounded + " | velocityY: " + rb.linearVelocity.y + " | isFalling: " + (!isGrounded && rb.linearVelocity.y < 0));
    }

    // Método público para que MagnetSystem active la animación
    public void SetMagnetAnimation(bool isActive)
    {
        animator.SetBool("isMagneting", isActive);
        Debug.Log("SetMagnetAnimation: " + isActive);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void HandleFlip()
    {
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}