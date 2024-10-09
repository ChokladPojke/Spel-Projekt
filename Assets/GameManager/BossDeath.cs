using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class 
    Death : MonoBehaviour
{
    public int bossPhaseID;
    public GameObject phase2;
    public GameObject phase3;

    private void Awake()
    {
        PlayerPrefs.SetInt("Phase3dead", 0);
    }

    public void BossDeath()
    {

       if(bossPhaseID == 1)
        {
            Instantiate(phase2, new Vector3(transform.position.x + 0.5f, (float)(transform.position.y - 0.5), 0), transform.rotation);
            Instantiate(phase2, new Vector3(transform.position.x -1 , (float)(transform.position.y - 0.5), 0), transform.rotation);

        }
        if (bossPhaseID == 2)
        {
            Instantiate(phase3, new Vector3(transform.position.x + 0.4f, (float)(transform.position.y - 0.6), 0), transform.rotation);
            Instantiate(phase3, new Vector3(transform.position.x - 1, (float)(transform.position.y - 0.4), 0), transform.rotation);


        }
        if(bossPhaseID == 3)
        {
            GameObject.Find("RoomController").GetComponent<RoomController>().phase3Kills += 1;
        }
    }
    
}
