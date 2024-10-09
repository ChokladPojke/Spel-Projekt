using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;

    public GameObject doorCollider;

    private GameObject[] players;
    private float widthOffset = 1.25f;
    private float widthOffset1 = 1.45f;
    public PlayerManager playerlist;
    private void Update()
    {
        playerlist = GameObject.Find("PlayerSpawner").GetComponent<PlayerManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                GameObject.Find("PlayerSpawner").GetComponent<PlayerInputManager>().enabled = false;
                switch (doorType)
                {
                    case DoorType.bottom:
                        other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y - widthOffset);
                        break;
                    case DoorType.left:
                        other.transform.position = new Vector2(other.transform.position.x - widthOffset1, other.transform.position.y);
                        break;
                    case DoorType.right:
                        other.transform.position = new Vector2(other.transform.position.x + widthOffset1, other.transform.position.y);
                        break;
                    case DoorType.top:
                        other.transform.position = new Vector2(other.transform.position.x, other.transform.position.y + widthOffset);
                        break;
                }
                foreach (GameObject player in playerlist.gameObjects)
                {
                    if (player != other.gameObject)
                        player.transform.position = other.transform.position;
                }
            }
    }
}
