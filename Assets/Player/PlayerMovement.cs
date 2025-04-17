using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float acceleration = 20f;
    public float deceleration = 25f;
    public float velocityPower = 0.9f;
    public float jumpingPower = 16f;
    private bool canDoubleJump = false;
    private bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private bool hasDashed = false;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;

    private bool isWallSliding = false;
    private bool isWallJumping = false;
    public float wallSlideSpeed = 2f;
    public float wallJumpXForce = 10f;
    public float wallJumpYForce = 14f;
    public float wallJumpLockTime = 0.2f;

    private bool isSliding = false;
    public float slideSpeed = 20f;  // Initial slide speed boost
    public float slideDecayRate = 0.95f; // How quickly slide slows down
    public float slideJumpBoost = 1.5f; // Jump boost multiplier if jumping early in slide
    private float currentSlideSpeed;

    public GameObject deathParticlesPrefab; // Drag the prefab here in the Inspector

    
    [SerializeField] private BoxCollider2D groundCheckCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D wallCheckColliderLeft;
    [SerializeField] private BoxCollider2D wallCheckColliderRight;
    private CircleCollider2D playerCollider;
    private Rigidbody2D rb;
    private TrailRenderer tr;
    private SpriteRenderer spriteRenderer; 
    private Animator animator;

    private void Start()
    {
        playerCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDashing)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        // Handle Sliding
        if (Input.GetKeyDown(KeyCode.S) && IsGrounded() && Mathf.Abs(horizontal) > 0)
        {
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            StopSlide();
        }

        // Wall Sliding Logic
        if (IsTouchingWall() && !IsGrounded() && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        else if (isWallSliding && horizontal != 0)
        {
            isWallSliding = false;
        }

        // Jumping Logic
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                if (isSliding && currentSlideSpeed > slideSpeed * 0.8f) // Boost jump if early in slide
                {
                    rb.velocity = new Vector2(rb.velocity.x * slideJumpBoost, jumpingPower);
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                }
                StopSlide();
            }
            else if (isWallSliding)
            {
                StartCoroutine(WallJump());
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                canDoubleJump = false;
            }
            animator.SetBool("isJumping", true);
        }

        // Reduce the jump height when releasing the button
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Dash (Disabled when wall sliding)
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !hasDashed && !isWallSliding)
        {
            StartCoroutine(Dash());
        }

        // Reset abilities when touching the ground
        if (IsGrounded())
        {
            hasDashed = false;
            canDoubleJump = true;
            isWallSliding = false;
        }

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        if (isDashing || isSliding)
            return;

        if (!isWallJumping)
        {
            ApplyAcceleration();
        }
    }

        IEnumerator DieCoroutine()
    {
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.2f); // Let the particles spawn properly
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D playerCollider)
    {
        if (playerCollider.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }

    private void ApplyAcceleration()
    {
        float targetSpeed = horizontal * speed;
        float speedDifference = targetSpeed - rb.velocity.x;
        float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, velocityPower) * Mathf.Sign(speedDifference);
        rb.AddForce(Vector2.right * movement, ForceMode2D.Force);
    }

    private void StartSlide()
    {
        animator.SetBool("Slide", true);
        tr.emitting = true;
        isSliding = true;
        currentSlideSpeed = slideSpeed * (isFacingRight ? 1 : -1);
        rb.velocity = new Vector2(currentSlideSpeed, rb.velocity.y);
    }

    private void StopSlide()
    {
        animator.SetBool("Slide", false);
        isSliding = false;
        tr.emitting = false;
    }

    private void FixedUpdateSlide()
    {
        if (isSliding)
        {
            currentSlideSpeed *= slideDecayRate;
            rb.velocity = new Vector2(currentSlideSpeed, rb.velocity.y);

            if (Mathf.Abs(currentSlideSpeed) < 2f) // Stop slide when speed is too low
            {
                StopSlide();
            }
        }
    }

    private bool IsTouchingWall()
    {
        return wallCheckColliderLeft.IsTouchingLayers(groundLayer) || wallCheckColliderRight.IsTouchingLayers(groundLayer);
    }

    private bool IsGrounded()
    {
        return groundCheckCollider.IsTouchingLayers(groundLayer);
    }

    private void Flip()
    {
        if (horizontal > 0 && !isFacingRight || horizontal < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private IEnumerator WallJump()
    {
        isWallSliding = false;
        isWallJumping = true;

        float jumpDirection = wallCheckColliderLeft.IsTouchingLayers(groundLayer) ? 1 : -1;
        rb.velocity = new Vector2(jumpDirection * wallJumpXForce, wallJumpYForce);

        yield return new WaitForSeconds(wallJumpLockTime);

        isWallJumping = false;
    }

    private IEnumerator Dash()
    {
        animator.SetBool("Slide", true);
        canDash = false;
        isDashing = true;
        hasDashed = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        float dashDirection = isFacingRight ? 1f : -1f;
        rb.velocity = new Vector2(dashDirection * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }
}