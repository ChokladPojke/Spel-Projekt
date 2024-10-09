using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Tilemaps;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{

    public static RoomController instance;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    Room currRoom;
    public GameManagerScript gms;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gms = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
    }

    public int phase3Kills = 0;
    void Update()
    {
        UpdateRoomQueue();
        UpdateRooms();
        if (phase3Kills == 4)
        {
            gms.victoryUI.SetActive(true);
        }
    }

    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                UpdateRooms();
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y) == true)
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                currentLoadRoomData.Y * room.Height,
                0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }

    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[] {
            "Empty",
            //"Basic1"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;

        StartCoroutine(RoomCoroutine());
    }

    public IEnumerator RoomCoroutine()
    {
        yield return new WaitForSeconds(0.001f);
        UpdateRooms();
    }

    public void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if (currRoom != room)
            {
                EnemyAI[] enemies = room.GetComponentsInChildren<EnemyAI>(); 
                
               

                if (enemies != null)
                {

                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        //door.doorCollider.SetActive(false);
                        // Backup utifall detta inte fungerar ^
                        door.enabled = true;
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        Transform childSpriteRenderer = door.transform.Find("Doors");
                        if (childSpriteRenderer != null)
                        {
                            childSpriteRenderer.gameObject.SetActive(true);
                    }
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.enabled = true;
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        Transform childSpriteRenderer = door.transform.Find("Doors");
                        if (childSpriteRenderer != null)
                        {
                            childSpriteRenderer.gameObject.SetActive(true);
                    }
                    }
                }
            }
            else // Rum-check
            {
                EnemyAI[] enemies = room.GetComponentsInChildren<EnemyAI>();
                foreach (EnemyAI e in enemies)
                {
                    e.WakeUp();
                }
                
                if (enemies.Length > 0)
                {
                    
                    foreach (Door door in room.GetComponentsInChildren<Door>()) //Allt ska bli true!
                    {
                        door.enabled = false;
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        Transform childSpriteRenderer = door.transform.Find("Doors");
                        if (childSpriteRenderer != null)
                        {
                            childSpriteRenderer.gameObject.SetActive(false);      
                        }
                    }
                }
                else
                {
                    foreach (Door door in room.GetComponentsInChildren<Door>())
                    {
                        door.enabled = true;
                        door.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                        Transform childSpriteRenderer = door.transform.Find("Doors");
                        if (childSpriteRenderer != null)
                        {
                            childSpriteRenderer.gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
}