using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StartScreenSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public Transform spawnPoint;
    

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnEnemy(5));
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator SpawnEnemy(float repeatDelay)
    {
        Random random = new Random();
        while (true)
        {
            Instantiate(enemyPrefabs[random.Next(0, enemyPrefabs.Length-1)], spawnPoint.position, spawnPoint.rotation);
            
            yield return new WaitForSeconds(repeatDelay);
        }
    }
}
