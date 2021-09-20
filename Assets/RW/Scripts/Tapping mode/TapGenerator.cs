using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TapGenerator : MonoBehaviour
{
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthInPoints;

    public List<GameObject> objects;

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    void AddObject(float lastObjectX)
    {
        int maxRange = ObjectPool.SharedInstance.amountOfBombsToPool + ObjectPool.SharedInstance.amountOfFishesToPool;
        //1
        int randomIndex = UnityEngine.Random.Range(0, maxRange - 1);
        GameObject obj;
        //2
        try
        {
            obj = ObjectPool.SharedInstance.GetPooledIObj(randomIndex);
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Sono un pesce e sono null");
            obj = ObjectPool.SharedInstance.GetPooledObject();
        }
  
        obj.SetActive(true);
        //3
        float objectPositionX = lastObjectX + UnityEngine.Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = UnityEngine.Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        //4
        objects.Add(obj);
    }

    void GenerateObjectsIfRequired()
    {
        //1
        float playerX = transform.position.x;
        float removeObjectsX = playerX - screenWidthInPoints;
        float addObjectX = playerX + screenWidthInPoints;
        float farthestObjectX = 0;
        //2
        List<GameObject> objectsToRemove = new List<GameObject>();
        foreach (var obj in objects)
        {
            //3
            float objX = obj.transform.position.x;
            //4
            farthestObjectX = Mathf.Max(farthestObjectX, objX);
            //5
            if (objX < removeObjectsX)
            {
                objectsToRemove.Add(obj);
            }
        }
        //6
        foreach (var obj in objectsToRemove)
        {
                objects.Remove(obj);
                obj.SetActive(false);
        }
        //7
        if (farthestObjectX < addObjectX)
        {
            AddObject(farthestObjectX);
        }
    }


    void AddRoom(float farthestRoomEndX)
    {
        GameObject room = ObjectPool.SharedInstance.GetPooledRoom();
       
        room.SetActive(true);
        float roomWidth = room.transform.Find("Floor").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;
        room.transform.position = new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);
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
        foreach (var room in currentRooms)
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
            currentRooms.Remove(room);
            room.SetActive(false);
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
            yield return new WaitForSeconds(0.25f);
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}