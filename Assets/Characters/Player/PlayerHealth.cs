using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D physicsCollider;
    private bool isAlive = true;
    public GameObject healthBarGO;
    public HealthBar healthbar;
    public GameManagerScript gameManager;

    public float iframes = 5;
    public void OnDeath(){
        Destroy(GameObject.Find("Player" + (GetComponent<PlayerController>().playerIndex + 1 + "Tag")));
        Destroy(GameObject.Find("Player" + (GetComponent<PlayerController>().playerIndex + 1 + "HP")));

        GameObject.Find("PlayerSpawner").GetComponent<PlayerInputManager>().enabled = false;
        GameObject.Find("PlayerSpawner").GetComponent<PlayerManager>().gameObjects.Remove(gameObject);
        Destroy(gameObject);

        //gameManager.GameOver(); 
    }
    
    public void Update(){
        iframes += Time.deltaTime;
    }

    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("hit");
            }

            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("isAlive", false);
                


            }
        }
        get
        {
            return _health;
        }
    }

    public float _health = 3f;
    public float max_health = 3f;

    public void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        animator.SetBool("isAlive", isAlive);
        max_health = _health;
        PlayerInput input = GetComponent<PlayerInput>();
        int playerIndex = input.playerIndex;
        healthBarGO = GameObject.Find("Player" + (playerIndex+1) + "HP");
        healthbar = healthBarGO.GetComponent<HealthBar>();

        healthbar.SetHealth(_health, max_health);
    }


    public void OnHit(float damage, Vector2 knockback)
    {
        if(iframes >= 1){
            Health -= damage;
            rb.AddForce(knockback);
            healthbar.SetHealth(_health, max_health);
            iframes = 0;
        }
    }



    void IDamageable.OnHit(float damage)
    {
        Health -= damage;
        healthbar.SetHealth(_health, max_health);
    }
}