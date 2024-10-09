using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject randomDecoration = child.gameObject;

            float randomValue = Random.value;

            if (randomDecoration.CompareTag("RandomSmallObjects"))
            {
                if (randomValue >= 0.3f)
                {
                    Destroy(randomDecoration);
                }
            }
            if (randomDecoration.CompareTag("RandomLargeObjects"))
            {
                if (randomValue >= 0.2f)
                {
                    Destroy(randomDecoration);
                }
            }
        }
    }
}
