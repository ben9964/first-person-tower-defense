using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance { get; set; }

    public GameObject playerHudPrefab;
    private WaveSpawner _waveSpawner;
    private AbstractPlayer _player;
    private bool _inMapView;
    private String _currentMap;

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

    public static void SetMap(String map)
    {
        Instance._currentMap = map;
    }

    public static String GetMap()
    {
        return Instance._currentMap;
    }
    
    public static void SetPlayer(AbstractPlayer player)
    {
        Instance._player = player;
    }

    public static AbstractPlayer GetPlayer()
    {
        return Instance._player;
    }

    public static bool HasPlayer()
    {
        return Instance._player != null;
    }
    
    public static void SetInMapView(bool inMapView)
    {
        Instance._inMapView = inMapView;
    }
    
    public static bool IsInMapView()
    {
        return Instance._inMapView;
    }
}
