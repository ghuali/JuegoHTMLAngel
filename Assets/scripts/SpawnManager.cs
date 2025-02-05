using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float startDelay = 1;
    private float spawnInterval = 1.5f; 
    void Start()
    {
       InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);  
    }
    private float spawnRangeX = -87;
    private float spawnRangeX2 = -88;
    private float spawnPosZ = -13;
    private float spawnPosZ2 = -22;
    

    public GameObject[] animalPrefabs;
    void Update()
    {
        
    }
    void SpawnRandomAnimal() {
    int animalIndex = Random.Range(0, animalPrefabs.Length);

  
        Vector3 spawnPos = new Vector3(Random.Range(spawnRangeX, spawnRangeX2), 0, Random.Range(spawnPosZ,spawnPosZ2));


       
        Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
