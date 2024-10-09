using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class EnemyAI : MonoBehaviour

{
    private Animator animator;

    private bool IsMoving
    {
        set
        {
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }
    private SpriteRenderer spriteRenderer;
    public GameObject Player;
    public float moveSpeed = 5f;
    private float distance;
    private bool canMove = true;
    private bool isMoving = false;
    public float attackTrigger = 2;
    private bool FoundPlayer = false;
    private float difficultySpeed;
    public float otherPlayerDistance = 5000;
    public GameObject targetPlayer;
    public PlayerManager playerManager;
    public float playerDistance;
    public XpController xp;
    public float wakeUpTimer;
    public bool enemyAwake = false;
    public void WakeUp()
    {
        enemyAwake = true;
    }
    private void Start()
    {
        xp = GameObject.Find("Experience Background").GetComponent<XpController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        difficultySpeed = PlayerPrefs.GetFloat("Difficulty");
        playerManager = GameObject.Find("PlayerSpawner").GetComponent<PlayerManager>();
        if (difficultySpeed == 1)
        {
            moveSpeed *= 0.7f;
        }
        else if (difficultySpeed == 2)
        {
            moveSpeed *= 1;
        }
        else if (difficultySpeed == 3)
        {
            moveSpeed *= 1.3f;
        }
    }
    private void Update()
    {
        if (enemyAwake == true)
        {
            wakeUpTimer += Time.deltaTime;
            if (wakeUpTimer >= 0.5f)
            {
                foreach (GameObject player in playerManager.gameObjects)
                {
                    playerDistance = Vector2.Distance(transform.position, player.transform.position);
                    if (playerDistance <= otherPlayerDistance)
                    {
                        targetPlayer = player;
                    }
                    otherPlayerDistance = playerDistance;
                }

                if (canMove == true)
                {
                    distance = Vector2.Distance(transform.position, targetPlayer.transform.position);
                    Vector2 direction = targetPlayer.transform.position - transform.position;

                    if (distance < 8)
                    {
                        IsMoving = true;
                        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, moveSpeed * Time.deltaTime);

                        if (distance < attackTrigger)
                            animator.SetTrigger("Attack");

                        if (direction.x > 0)
                        {
                            spriteRenderer.flipX = false;
                            gameObject.BroadcastMessage("IsFacingRight", true);
                        }
                        else if (direction.x < 0)
                        {
                            spriteRenderer.flipX = true;
                            gameObject.BroadcastMessage("IsFacingRight", false);
                        }
                    }
                    else
                    {
                        IsMoving = false;
                    }
                }
            }
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

    public void FuckingDie()
    {
        xp.GetXP();
        Destroy(gameObject);
    }
    public void BossDie()
    {
        Destroy(gameObject);
    }
}