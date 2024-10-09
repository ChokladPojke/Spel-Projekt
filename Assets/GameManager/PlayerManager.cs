using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public bool gameActive = false;
    public GameManagerScript gameManager; 

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    void Update()
    {
        transform.position = new Vector2(Random.Range(-6.6f, 6.6f), Random.Range(2.5f,-2.5f));
        if (gameActive)
        {
            if(gameObjects.Count == 0)
            {
                gameManager.GameOver();
            }
        }
    }

    public void PlayerJoin()
    {
        gameActive = true;
        GameObject newPlayer;
        int newPlayerID = PlayerPrefs.GetInt("Players");
        newPlayer = GameObject.Find("player" + newPlayerID);
        newPlayer.transform.position = gameObject.transform.position;
        gameObjects.Add(newPlayer);
    }
}

