using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 1000f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.GetComponent<EnemyAI>().wakeUpTimer >= 0.5f)
        {
            Collider2D collider = col.collider;
            if (col.gameObject.CompareTag("Player"))
            {
                IDamageable damageableObject = collider.GetComponent<IDamageable>();

                if (damageableObject != null)
                {
                    // Checks for an enemy hitbox and sends dmg to GameObject
                    Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

                    Vector2 direction = (Vector2)(collider.gameObject.transform.position - transform.position).normalized;

                    Vector2 knockback = direction * knockbackForce;

                    damageableObject.OnHit(damage, knockback);
                }
            }
        }
    }
}