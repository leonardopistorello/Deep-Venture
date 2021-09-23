using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> deactiveObjects;
    public List<GameObject> activeObjects;
    public List<GameObject> deactiveRooms;
    public List<GameObject> activeRooms;
    public List<GameObject> FishesToPool;
    public GameObject room;
    public GameObject bomb;
    public GameObject seaweed;
    public int amountOfFishesToPool;
    public int amountOfBombsToPool;
    public int amountOfRooms;
    public int amountOfSeaweed;
    private void Awake()
    {
        SharedInstance = this;
    }

    // Questo metodo ritorna tutti gli oggetti che sono ancora nel pool
    public GameObject GetDeactiveObject(int index)
    {
        return deactiveObjects[index];
    }

    public GameObject GetDeactiveRoom(int index)
    {
       return deactiveRooms[index];
    }

    private void Start()
    {
        deactiveObjects = new List<GameObject>();
        GameObject tmp;
        foreach (GameObject g in FishesToPool)
        {
            for (int i = 0; i < amountOfFishesToPool; i++)
            {
                tmp = Instantiate(g);
                tmp.SetActive(false);
                deactiveObjects.Add(tmp);
            }
        }

        for (int i = 0; i < amountOfBombsToPool; i++)
        {
            tmp = Instantiate(bomb);
            tmp.SetActive(false);
            deactiveObjects.Add(tmp);
        }
        
        for (int i = 0; i < amountOfRooms; i++)
        {
            tmp = Instantiate(room);
            tmp.SetActive(false);
            deactiveRooms.Add(tmp);
        }

        for (int i = 0; i < amountOfSeaweed; i++)
        {
            tmp = Instantiate(seaweed);
            tmp.SetActive(false);
            deactiveObjects.Add(tmp);
        }
    }
}