using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapGenerator : MonoBehaviour
{
    private float screenWidthInPoints;
    public GameObject player;

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    public float bombSpawnTime;
    public float fishSpawnTime;
    public float seaweedSpawnTime;

    void AddFish()
    {
        float playerX = player.transform.position.x;
        int maxRange = ObjectPool.SharedInstance.disabledFishes.Count;
        //1
        int randomIndex = UnityEngine.Random.Range(0, maxRange);
        GameObject obj = ObjectPool.SharedInstance.GetDisabledFishes(randomIndex);
  
        
        //3
        float objectPositionX = playerX + UnityEngine.Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = UnityEngine.Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        //4
        obj.SetActive(true);
        ObjectPool.SharedInstance.activeFishes.Add(obj);
        ObjectPool.SharedInstance.disabledFishes.Remove(obj);
    }

    void AddBomb()
    {
        float playerX = player.transform.position.x;
        int maxRange = ObjectPool.SharedInstance.disabledBombs.Count;
        //1
        int randomIndex = UnityEngine.Random.Range(0, maxRange);
        GameObject obj = ObjectPool.SharedInstance.GetDisabledBombs(randomIndex);
        //3
        float objectPositionX = playerX + UnityEngine.Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = UnityEngine.Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        obj.SetActive(true);
        //4
        ObjectPool.SharedInstance.activeBombs.Add(obj);
        ObjectPool.SharedInstance.disabledBombs.Remove(obj);
    }

    void AddSeaweed()
    {
        float playerX = player.transform.position.x;
        int maxRange = ObjectPool.SharedInstance.disabledSeaweeds.Count;
        //1
        int randomIndex = UnityEngine.Random.Range(0, maxRange);
        GameObject obj = ObjectPool.SharedInstance.GetDisabledSeaweed(randomIndex);

        //3
        float objectPositionX = playerX + UnityEngine.Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = UnityEngine.Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        obj.SetActive(true);
        //4
        ObjectPool.SharedInstance.activeSeaweeds.Add(obj);
        ObjectPool.SharedInstance.disabledSeaweeds.Remove(obj);
        
    }

    void GenerateObjectsIfRequired()
    {
        //1
        float playerX = player.transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestBombX = 0;
        float farthestSeaweedX = 0;
        //2
        /* Aggiunte due liste e cambiati nomi 
           Era solo una lista di oggetti da rimuovere
           e for each per ogni nuova lista*/
        List<GameObject> bombsToRemove = new List<GameObject>();
        List<GameObject> seaweedToRemove = new List<GameObject>();
        foreach (var obj in ObjectPool.SharedInstance.activeBombs)
        {
            //3
            float objX = obj.transform.position.x;
            //4
            farthestBombX = Mathf.Max(farthestBombX, objX); 
            //5
            if (objX < removeObjectsX)
            {
                bombsToRemove.Add(obj);
            }
        }

        foreach (var obj in ObjectPool.SharedInstance.activeSeaweeds)
        {
            //3
            float objX = obj.transform.position.x;
            //4
            farthestSeaweedX = Mathf.Max(farthestSeaweedX, objX);
            //5
            if (objX < removeObjectsX)
            {
                seaweedToRemove.Add(obj);
            }
        }

        //6
        foreach (var obj in bombsToRemove)
        {
            ObjectPool.SharedInstance.activeBombs.Remove(obj);
            ObjectPool.SharedInstance.disabledBombs.Add(obj);
            obj.SetActive(false);
        }

        foreach (var obj in seaweedToRemove)
        {
            ObjectPool.SharedInstance.activeSeaweeds.Remove(obj);
            ObjectPool.SharedInstance.disabledSeaweeds.Add(obj);
        }
    }


    void AddRoom(float farthestRoomEndX)
    {
        int randomRoomIndex = UnityEngine.Random.Range(0, ObjectPool.SharedInstance.deactiveRooms.Count);
        GameObject room = ObjectPool.SharedInstance.GetDisabledRooms(randomRoomIndex);
        room.SetActive(true);
        float roomWidth = room.transform.Find("Floor").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0, 0);
        ObjectPool.SharedInstance.deactiveRooms.Remove(room);
        ObjectPool.SharedInstance.activeRooms.Add(room);
        
    }

    private void GenerateRoomIfRequired()
    {
        //1
        List<GameObject> roomsToRemove = new List<GameObject>();
        //2
        bool addRooms = true;
        //3
        float playerX = transform.position.x;
        //4
        float removeRoomX = playerX - screenWidthInPoints;
        //5
        float addRoomX = playerX + screenWidthInPoints;
        //6
        float farthestRoomEndX = 0;
        foreach (var room in ObjectPool.SharedInstance.activeRooms)
        {
            //7
            float roomWidth = room.transform.Find("Floor").localScale.x;
            float roomStartX = room.transform.position.x - (roomWidth * 0.5f);
            float roomEndX = roomStartX + roomWidth;
            //8
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            //9
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            //10
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        //11
        foreach (var room in roomsToRemove)
        {
            ObjectPool.SharedInstance.activeRooms.Remove(room);
            room.SetActive(false);
            ObjectPool.SharedInstance.deactiveRooms.Add(room);
        }
        //12
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }


    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            GenerateObjectsIfRequired();
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator GeneratorFish()
    {
        while(true)
        {
            AddFish();
            yield return new WaitForSeconds(fishSpawnTime);
        }
    }

    private IEnumerator GeneratorSeeweed()
    {
        while(true)
        {
            AddSeaweed();
            yield return new WaitForSeconds(seaweedSpawnTime);
        }
    }

    private IEnumerator GeneratorBomb()
    {
        while(true)
        {
            AddBomb();
            yield return new WaitForSeconds(bombSpawnTime);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        /*
         * Qui si calcola la dimensione dello schermo in punti. 
         * Le dimensioni dello schermo verranno utilizzate per determinare 
         * se � necessario generare una nuova stanza.
         */
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;
        StartCoroutine(GeneratorCheck());
        StartCoroutine(GeneratorFish());
        StartCoroutine(GeneratorSeeweed());
        StartCoroutine(GeneratorBomb());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}