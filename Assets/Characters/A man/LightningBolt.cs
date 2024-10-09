using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public float timeAlive = 0;
    public Animator animator;

    public float lightningBoltDamage = 2f;
    public float knockbackForce = 1500f;
    public bool isAlive = true;
    public float slowdown = 1;
    void Start()
    {
        animator.SetBool("Idle", true);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DamageableCharacter enemy = collider.GetComponent<DamageableCharacter>();

        if (collider.CompareTag("Enemy"))
        {
            animator.SetBool("Contact", true);
        }
        if (collider.CompareTag("Wall")){
            animator.SetBool("Contact", true);
            isAlive = false;
            slowdown = 0.1f;
        }

        Vector2 direction = (Vector2)(collider.gameObject.transform.position - transform.position).normalized;
        enemy.OnHit(lightningBoltDamage, direction);
    }
    
        void Update()
    {
        timeAlive += Time.deltaTime;
        transform.Translate(Vector3.right * Time.deltaTime * 10 * slowdown);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}