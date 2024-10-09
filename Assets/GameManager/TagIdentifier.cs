using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TagIdentifier : MonoBehaviour
{
    private void Start()
    {
        if (gameObject.name == "Player1Tag")
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.red;
        }
        if (gameObject.name == "Player2Tag")
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.blue;
        }
        if (gameObject.name == "Player3Tag")
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.yellow;
        }
        if (gameObject.name == "Player4Tag")
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = Color.green;
        } 
    }
}
