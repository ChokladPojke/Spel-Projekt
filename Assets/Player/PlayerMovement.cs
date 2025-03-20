using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool canDoubleJump = false;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private bool hasDashed = false;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D groundCheckCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D wallCheckCollider;
    [SerializeField] private TrailRenderer tr;

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        // Grounded Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        // Wall Jump
        else if (Input.GetButtonDown("Jump") && CanWallJump())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        // Double Jump (Only allowed if not grounded)
        else if (Input.GetButtonDown("Jump") && !IsGrounded() && canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            canDoubleJump = false; // Consume double jump
        }

        // Reduce the jump height when the jump button is released
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !hasDashed)
        {
            StartCoroutine(Dash());
        }

        // Reset abilities when touching the ground
        if (IsGrounded())
        {
            hasDashed = false;
            canDoubleJump = true; // Reset double jump when landing
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool CanWallJump()
    {
        return wallCheckCollider.IsTouchingLayers(groundLayer) && !IsGrounded();
    }

    private bool IsGrounded()
    {
        return groundCheckCollider.IsTouchingLayers(groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        hasDashed = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);
        
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}