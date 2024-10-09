using Unity.VisualScripting;
using UnityEngine;

public class DamageableCharacter : MonoBehaviour, IDamageable
{
    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D physicsCollider;
    public HealthBar healthBar;
    public RoomController roomController;
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
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCoroutine());
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

    public void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
        max_health = _health;
        healthBar.SetHealth(_health, max_health);
        roomController = GameObject.Find("RoomController").GetComponent<RoomController>();
    }


    public void OnHit(float damage, Vector2 knockback)
    {
        Health -= damage;
        rb.AddForce(knockback);
        healthBar.SetHealth(_health, max_health);
    }

    public void OnHit(float damage)
    {
        Health -= damage;
    }
}