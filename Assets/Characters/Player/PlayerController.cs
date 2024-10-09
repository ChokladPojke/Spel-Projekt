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
    //Menu
    public GameManagerScript gameManager;
    public void OnMenu()
    {
        gameManager.GameMenu();
    }


    //Movement
    public float moveSpeed = 1000f;
    public float maxSpeed = 15f;
    private bool isMoving = false;
    private bool canMove = true;
    private float dashTimer = 1;
    public float dashCD = 0.75f;


    public GameObject swordHitbox;
    public GameObject lightningBoltSpawner;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D swordCollider;
    private Vector2 moveInput = Vector2.zero;
    public Canvas canvas;
    public GameObject playerHP;
    public int classID = 0; //1 �r night, 2 �r a man
    public Quaternion rotation;
    public GameObject ChSelect;
    public int playerIndex;
    private void Start()
    {
        ChSelect = GameObject.Find("CharacterSelect");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        PlayerInput input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        playerIndex = input.playerIndex;
        gameObject.name = "player" + (playerIndex + 1);
        PlayerPrefs.SetInt("Players", playerIndex + 1);
        GameObject.Find("PlayerSpawner").GetComponent<PlayerManager>().PlayerJoin();

    }

    bool playerList = false;
    public void OnPlayerList()
    {

        if (playerList)
        {
            foreach (TagIdentifier playerTag in GameObject.Find("TagManager").GetComponentsInChildren<TagIdentifier>())
            {
                playerTag.gameObject.GetComponent<TextMeshProUGUI>().fontSize = 0;

                playerList = false;
            }
        }
        else
        {
            foreach (TagIdentifier playerTag in GameObject.Find("TagManager").GetComponentsInChildren<TagIdentifier>())
            {
                playerTag.gameObject.GetComponent<TextMeshProUGUI>().fontSize = 36;
            }
            playerList = true;
        }




    }

    private void FixedUpdate()
    {
        rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(0 - moveInput.y, moveInput.x, 0f));
        if (dashTimer <= dashCD)
        dashTimer += Time.deltaTime;
        if (classID != 0)
        {
            GameObject.Find("Player" + (playerIndex + 1) + "Tag").transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
            ChSelect.GetComponentInChildren<TextMeshProUGUI>().enabled = false;

            foreach (Image image in ChSelect.GetComponentsInChildren<Image>())
            {
                image.enabled = false;
            }

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
        else
        {
            ChSelect.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            foreach (Image image in ChSelect.GetComponentsInChildren<Image>())
            {
                image.enabled = true;
            }
        }

    }



    public void OnHeroKnight()
    {
        if (classID == 0)
        {
            classID = 1;
            animator.SetInteger("ClassID", 1);
        }
    }
    public void OnAman()
    {
        if (classID == 0)
        {
            classID = 2;
            animator.SetInteger("ClassID", 2);
            foreach (BoxCollider2D bC2D in gameObject.GetComponents<BoxCollider2D>())
            {
                bC2D.offset = new Vector2(0, -0.25f);
            }
        }
    }

    //Get input value for player movement
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Quaternion qValue = value.Get<Quaternion>();
    }

    public void OnFire()
    {
        if (Time.timeScale == 1)
        {
            animator.SetTrigger("swordAttack");
        }
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnLockMovement()
    {
        canMove = true;
    }

    public void OnDash()
    {
        if (classID == 1)
        {
            if (dashTimer >= dashCD && Time.timeScale == 1)
            {
                animator.SetTrigger("Dash");
            }
        }
        if (classID == 2)
        {
            if (canMove && Time.timeScale == 1)
            {
                animator.SetTrigger("Dash");
            }
        }

    }
    public void OnFireball()
    {
        LightningBoltSpawner lbs = GetComponentInChildren<LightningBoltSpawner>();
        lbs.aimDir = rotation;
        lbs.ShootProjectile();
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