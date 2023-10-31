using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Transform spawnPoint;
    

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(SpawnEnemy(5));
    }

    IEnumerator SpawnEnemy(float repeatDelay)
    {
        while (true)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            yield return new WaitForSeconds(repeatDelay);
        }
    }
}
