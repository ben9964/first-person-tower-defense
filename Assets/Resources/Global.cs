using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance { get; set; }

    public GameObject playerHudPrefab;
    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        Instance = this;
    }

    public static GameObject GetHudPrefab()
    {
        return Instance.playerHudPrefab;
    }

    public static void SetWaveSpawner(WaveSpawner spawner)
    {
        Instance._waveSpawner = spawner;
    }

    public static WaveSpawner GetWaveSpawner()
    {
        return Instance._waveSpawner;
    }
}
