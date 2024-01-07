using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupManager : MonoBehaviour
{
    public float xMin;
    public float xMax;
    
    public float zMin;
    public float zMax;

    public GameObject[] powerupPrefabs;
    
    private void Start()
    {
        InvokeRepeating("SpawnPowerup", 0, 60);
    }
    
    private void SpawnPowerup()
    {
        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);
        Vector3 spawnPos = new Vector3(x, 0, z);
        spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos) + 3;
        GameObject powerup = Instantiate(powerupPrefabs[Random.Range(0, powerupPrefabs.Length)], spawnPos, Quaternion.identity);
        powerup.SetActive(true);
    }
}
