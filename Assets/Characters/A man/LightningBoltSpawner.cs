using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltSpawner : MonoBehaviour
{

    private Vector3 faceRight = new Vector3(0.7f, 0.4f, 0);
    private Vector3 faceLeft = new Vector3(-0.7f, 0.4f, 0);
    public float boltDMG;
    public GameObject lightningBolt;
    public Quaternion aimDir;
    
    public void ShootProjectile()
    {
        GameObject newObject = Instantiate (lightningBolt, transform.position, aimDir);
        newObject.GetComponent<LightningBolt>().lightningBoltDamage = boltDMG;
    }


    public void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
}
