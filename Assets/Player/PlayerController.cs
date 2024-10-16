using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    //Movement
    public float moveSpeed = 1000f;
    public float maxSpeed = 1f;
    private bool isMoving = false;
    private bool canMove = true;
    private float dashTimer = 1;
    public float dashCD = 0.75f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput = Vector2.zero;
    public Quaternion rotation;
    public int playerIndex;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(0 - moveInput.y, moveInput.x, 0f));
        if (dashTimer <= dashCD)
        dashTimer += Time.deltaTime;

            //movement controller
            if (canMove == true && moveInput != Vector2.zero)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveSpeed * Time.deltaTime * moveInput), maxSpeed);

                // Set direction of sprite to movement direction and swordhitbox
                if (moveInput.x > 0)
                {
                    spriteRenderer.flipX = false;
                    gameObject.BroadcastMessage("IsFacingRight", true);
                }
                else if (moveInput.x < 0)
                {
                    spriteRenderer.flipX = true;
                    gameObject.BroadcastMessage("IsFacingRight", false);
                }
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }
        }

    //Get input value for player movement
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Quaternion qValue = value.Get<Quaternion>();
    }

    public void OnDash()
    {
        if (dashTimer >= dashCD && Time.timeScale == 1){
            animator.SetTrigger("Dash");
        }

    }

    public void DashSpeed()
    {
        if (dashTimer >= 0.75f)
        {
            maxSpeed *= 2;
        }
    }
    public void OnDashExit()
    {
        if (dashTimer > 0.75f)
        {
            maxSpeed /= 2;
            dashTimer = 0;
        }
    }
}