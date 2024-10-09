using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 1500f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(1f,0f,0);
    public Vector3 faceLeft = new Vector3(-1f,0f,0);


    private void OnTriggerEnter2D(Collider2D collider) {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        
        if(damageableObject != null){

            Vector3 parentPosition = transform.parent.position;

            Vector2 direction =(Vector2) (collider.gameObject.transform.position - transform.position).normalized;

            Vector2 knockback = direction * knockbackForce;

            damageableObject.OnHit(swordDamage, knockback);
        }
    }

    public void IsFacingRight(bool isFacingRight){
        if(isFacingRight) {
            gameObject.transform.localPosition = faceRight;
        } else {
            gameObject.transform.localPosition = faceLeft;
        }
    }


    

}
