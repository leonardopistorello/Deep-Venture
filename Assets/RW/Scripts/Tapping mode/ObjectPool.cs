using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public List<GameObject> pooledRooms;
    public List<GameObject> FishesToPool;
    public GameObject room;
    public GameObject BombToPool;
    public int amountOfFishesToPool;
    public int amountOfBombsToPool;
    public int amountOfRooms;

    private void Awake()
    {
        SharedInstance = this;
    }

    // Questo metodo ritorna tutti gli oggetti che sono ancora nel pool
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < (amountOfBombsToPool + amountOfFishesToPool); i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public GameObject GetPooledRoom()
    {
        for (int i = 0; i < amountOfRooms; i++)
        {
            if(!pooledRooms[i].activeInHierarchy)
            {
                return pooledRooms[i];
            }
        }
        return null;
    }

    public GameObject GetPooledIObj(int index)
    {
        if (!pooledObjects[index].activeInHierarchy)
        {
            return pooledObjects[index];
        }
        return null;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        foreach (GameObject g in FishesToPool)
        {
            for (int i = 0; i < amountOfFishesToPool; i++)
            {
                tmp = Instantiate(g);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        for (int i = 0; i < amountOfBombsToPool; i++)
        {
            tmp = Instantiate(BombToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        
        for (int i = 0; i < amountOfRooms; i++)
        {
            tmp = Instantiate(room);
            tmp.SetActive(false);
            pooledRooms.Add(tmp);
        }
    }
}