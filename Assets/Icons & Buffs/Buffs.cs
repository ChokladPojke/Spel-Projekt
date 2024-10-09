using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    List<string> stringList = new()
    {
        "Speed",
        "Speed(Clone)",
        "Damage",
        "Damage(Clone)",
        "Shield",
        "Shield(Clone)"
    };
    public PlayerManager playerList;
    public GameObject speedParticle;
    public GameObject damageParticle;

    public float Duration = 5;
    public float activeDuration = 0;

    public float speedBuff = 1.5f;
    public float damageBuff = 2f;
    public float shieldBuff = 1f;

    private int buffType;

    public bool ActiveBuff;
    public bool pickedUp = false;


    public void Start(){
        playerList = GameObject.Find("PlayerSpawner").GetComponent<PlayerManager>();
    }
    void Update()
    {
        if (ActiveBuff)
        {
            activeDuration += Time.deltaTime;
            if (activeDuration >= Duration)
            {
                ResetBuffs();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && !pickedUp ){
            {
                Destroy(GetComponentInChildren<ParticleSystem>().gameObject);
                pickedUp = true;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                ActiveBuff = true;

                // Automatically determine buff type based on stringList
                buffType = stringList.IndexOf(gameObject.name) + 1;

                ApplyBuff();
            }
        }
    }

    void ApplyBuff()
    {
        switch (buffType)
        {
            case 1 or 2:
                SpeedBuff();
                break;
            case 3 or 4:
                DamageBuff();
                break;
            case 5 or 6:
                ShieldBuff();
                break;
            default:
                Debug.LogWarning("Unknown buff type. gameObjects name has to be either Speed, Damage or Shield");
                break;
        }
    }

    void SpeedBuff()
    {
        foreach(GameObject player in playerList.gameObjects){
            player.GetComponent<PlayerController>().maxSpeed *= speedBuff;
            GameObject speed = Instantiate(speedParticle, player.transform.position, player.transform.rotation);
            speed.GetComponent<SpeedBuffScript>().TargetPlayer = player;
            Destroy(GetComponent<ParticleSystem>());
        }
    }

    void DamageBuff()
    {
        foreach(GameObject player in playerList.gameObjects){
            player.GetComponentInChildren<SwordAttack>().swordDamage *= damageBuff;
            player.GetComponentInChildren<LightningBoltSpawner>().boltDMG *= damageBuff;
            GameObject damage = Instantiate(damageParticle, player.transform.position, player.transform.rotation);
            damage.GetComponent<DamageBuffScript>().TargetPlayer = player;
            Destroy(GetComponent<ParticleSystem>());
        }
    }

    void ShieldBuff()
    {
        foreach(GameObject player in playerList.gameObjects){
            player.GetComponent<PlayerHealth>()._health += shieldBuff;
        }
    }

    void ResetBuffs()
    {
        switch (buffType)
        {
            
            case 1 or 2:
                foreach(GameObject player in playerList.gameObjects){
                    player.GetComponent<PlayerController>().maxSpeed /= speedBuff;
                    Destroy(player.GetComponentInChildren<ParticleSystem>().gameObject);

                }
                break;
            case 3 or 4:
                foreach(GameObject player in playerList.gameObjects){
                    player.GetComponentInChildren<SwordAttack>().swordDamage /= damageBuff;
                    player.GetComponentInChildren<LightningBoltSpawner>().boltDMG /= damageBuff;
                    Destroy(player.GetComponentInChildren<ParticleSystem>().gameObject);
                }
                break;
        }
    }
}
