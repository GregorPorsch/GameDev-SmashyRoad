using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawn : MonoBehaviour
{
    public GameObject[] policeSpawnAreas;
    public GameObject[] policePrefabs;
    public float minDistance = 1000;
    public int startAmountPoliceCars = 5;
    public int timeForSwat = 30;
    float lastTimeSpwaned;
    Vector3 playerPosition;
    float timePlayed;

    // Start is called before the first frame update
    void Start()
    {
        timePlayed = Time.time;
        playerPosition = PlayerDamageController.playerPosition;
        int[] areasSpawned = new int[startAmountPoliceCars];
        for (int i = 0; i < startAmountPoliceCars; i++)
        {
            int spawnArea = 0;
            do
            {
                spawnArea = electSpawnArea();
            } while (iteamInArray(areasSpawned, spawnArea));
            
            areasSpawned[i] = spawn(spawnArea);
        }
        lastTimeSpwaned = Time.time;

        /* for (int i = 0; i < areasSpawned.Length; i++)
        {
            Debug.Log(areasSpawned[i]);
        } */
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = PlayerDamageController.playerPosition;
        if (Time.time > lastTimeSpwaned + 20)
        {
            spawn(electSpawnArea());
            lastTimeSpwaned = Time.time;
        }
    }

    public bool iteamInArray(int[] array, int item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public int electSpawnArea()
    {
        List<int> spawnPossibilities = checkMinDistance();
        return Random.Range(0, spawnPossibilities.Count);
    }

    public int spawn(int spawnArea)
    {
        Instantiate(policePrefabs[choosePrefab()], policeSpawnAreas[spawnArea].transform.position, Quaternion.Euler(0,0,0));
        return spawnArea;
    }

    List<int> checkMinDistance()
    {
        List<int> spawnPossibilities = new List<int>();
        for (int i = 0; i < policeSpawnAreas.Length; i++)
        {
            if (Vector3.Distance(policeSpawnAreas[i].transform.position, playerPosition) >= minDistance)
            {
                spawnPossibilities.Add(i);
            }
        }
        return spawnPossibilities;
    }

    public int choosePrefab()
    {
        if ((Time.time - timePlayed) > timeForSwat)
        {
            return Random.Range(0, 2);
        }
        else
        {
            return 0;
        }
    }
}
