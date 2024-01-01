using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SackSpawner : MonoBehaviour
{
    public GameObject moneyPrefab;
    public GameObject[] moneySpawnAreas;
    public int sackAnzahl = 10;
    List<int> lastSpawnAreaNumber;
    int spawnAreaNumber;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnAreaNumber = new List<int>();
        for (int i = 0; i < sackAnzahl; i++)
        {
            spawn();
        }
    }

    public void spawn()
    {
        do
        {
            spawnAreaNumber = Random.Range(0, moneySpawnAreas.Length);
        } while (lastSpawnAreaNumber.Contains(spawnAreaNumber));
        Instantiate(moneyPrefab, moneySpawnAreas[spawnAreaNumber].transform.position, Quaternion.identity);
        lastSpawnAreaNumber.Add(spawnAreaNumber);
        // lastSpawnAreaNumber.Remove(0);
    }
}
