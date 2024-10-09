using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuffScript : MonoBehaviour
{
    public GameObject TargetPlayer;

    private void Update()
    {
        transform.position = TargetPlayer.transform.position;
        transform.parent = TargetPlayer.transform;
    }
}
