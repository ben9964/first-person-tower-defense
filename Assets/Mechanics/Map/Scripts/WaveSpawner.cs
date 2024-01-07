// This script manages the spawning of waves of enemies in the game.
// It also updates relevant UI elements to inform the player about the wave status.

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;  // Importing the namespace for TextMeshPro, which is used for UI text elements.
using UnityEngine;
using Random = System.Random;
using AYellowpaper.SerializedCollections;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    [SerializedDictionary("Enemy Name", "Enemy Prefab")]
    public SerializedDictionary<String, GameObject> enemyTypes;

    // Reference to the location where enemies will be spawned.
    public Transform spawnPoint;

    // The number of the current wave.
    private int _waveNumber = 0;
    private int enemiesSpawned = 0;
    private int enemiesAlive = 0;

    private bool inWave = false;

    private Dictionary<String, Dictionary<Int32, List<String>>> waves = new(){
        {
            "Easy", new(){
                {1, new List<String>{"regular", "regular", "regular", "regular", "regular", "regular"}},
                {2, new List<String>{"regular", "regular", "slow", "regular", "regular", "slow"}},
                {3, new List<String>{"fast"}},
                {4, new List<String>{"slow", "slow", "fast", "regular", "regular", "regular", "regular", "regular", "fast", "slow"}},
                {5, new List<String>{"fast", "fast", "regular", "fast", "fast", "regular", "fast", "regular", "slow", "slow"}},
            }
        },
        {
            "Medium", new(){
                {1, new List<String>{"regular", "regular", "regular", "regular", "regular", "regular"}},
                {2, new List<String>{"regular", "regular", "slow", "regular", "regular", "slow"}},
                {3, new List<String>{"fast", "fast", "fast"}},
                {4, new List<String>{"slow", "slow", "fast", "regular", "regular", "regular", "regular", "regular", "fast", "slow"}},
                {5, new List<String>{"fast", "fast", "regular", "fast", "fast", "regular", "fast", "regular", "slow", "slow"}},
                {6, new List<String>{"fast", "fast", "fast", "fast", "fast", "fast", "fast", "fast", "fast", "fast"}},
                {7, new List<String>{"slow", "slow", "slow", "fast", "regular", "fast", "fast", "slow", "slow", "regular", "regular", "regular", "regular"}},
                {8, new List<String>{"slow", "regular", "regular", "regular", "slow", "regular", "regular", "regular", "slow", "regular", "fast", "fast", "regular", "regular", "regular"}}
            }
        },
        {
            "Hard", new(){
                {1, new List<String>{"regular", "regular", "regular", "regular", "regular"}},
                {2, new List<String>{"slow", "regular", "slow", "regular", "slow", "regular", "regular", "regular", "regular"}},
                {3, new List<String>{"fast", "regular", "fast", "regular", "fast", "regular", "slow", "regular", "fast", "fast", "regular", "regular"}},
                {4, new List<String>{"fast", "fast", "fast", "fast", "fast", "fast", "fast", "fast", "fast", "fast"}},
                {5, new List<String>{"slow", "slow", "slow", "fast", "regular", "fast", "fast", "slow", "slow", "regular", "regular", "regular", "regular"}},
                {6, new List<String>{"slow", "regular", "regular", "regular", "slow", "regular", "regular", "regular", "slow", "regular", "fast", "fast", "regular", "regular", "regular"}},
                {7, new List<String>{"fast", "fast", "regular", "regular", "slow", "regular", "fast", "fast", "slow", "regular", "fast", "fast", "slow", "regular", "slow", "regular", "regular", "regular", "slow", "slow"}},
                {8, new List<String>{"slow", "slow", "regular", "regular", "slow", "regular", "slow", "regular", "regular", "regular", "slow", "slow", "fast", "fast", "regular", "regular", "fast", "regular", "slow", "regular", "fast", "fast", "slow", "slow"}},
                {9, new List<String>{"fast", "fast", "regular", "regular", "slow", "regular", "fast", "regular", "slow", "regular", "fast", "fast", "fast", "regular", "slow", "slow", "slow", "regular", "slow", "slow", "fast", "fast", "slow", "regular", "regular", "slow", "slow"}},
                {10, new List<String>{"slow", "slow", "regular", "regular", "fast", "fast", "slow", "regular", "slow", "regular", "fast", "fast", "fast", "regular", "slow", "slow", "fast", "regular", "slow", "slow", "slow", "regular", "slow", "slow", "regular", "regular", "fast", "fast", "fast", "slow"}},
                {11, new List<String>{"fast", "fast", "regular", "regular", "regular", "slow", "slow", "fast", "fast", "slow", "regular", "regular", "slow", "slow", "regular", "regular", "slow", "slow", "fast", "fast", "regular", "slow", "regular", "slow", "fast", "regular", "regular", "slow", "slow", "slow", "fast", "fast", "regular", "regular"}},
                {12, new List<String>{"slow", "slow", "regular", "regular", "fast", "fast", "slow", "regular", "fast", "fast", "fast", "regular", "slow", "slow", "slow", "regular", "fast", "fast", "slow", "regular", "regular", "slow", "slow", "regular", "fast", "slow", "slow", "slow", "fast", "fast", "slow", "regular", "slow", "regular", "regular", "fast", "fast", "slow", "regular", "slow", "slow", "fast", "fast", "regular"}}
            }
        }
    }; 

    // Called when the game starts.
    void Start()
    {
        // Initial setup can be done here if needed.
        Global.GetPlayer().GetHud().UpdateWaveText(_waveNumber, waves[Global.GetMap()].Count);
        spawnPoint = GameObject.FindWithTag("Start").transform;
    }

    // Function to handle the spawning of a wave of enemies.
    public void NextWave()
    {
        _waveNumber++;  // Increase the wave number for this wave
        inWave = true;
        enemiesSpawned = 0;
        enemiesAlive = 0;
        Global.GetPlayer().GetHud().UpdateWaveText(_waveNumber, waves[Global.GetMap()].Count);
        StartCoroutine(SpawnEnemies(_waveNumber));
    }

    private void _win()
    {
        AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>();
        player.GetHud().SendMessage("You Win, Returning to the selection menu...", new Color32(0, 255, 0, 255));
        player.Freeze();
        GameSavingManager.instance.SaveGameData();
        this.Invoke(() =>
        {
            SceneManager.LoadScene("CharacterSelect");
        }, 2f);
    }
    
    IEnumerator SpawnEnemies(int wave)
    {
        Random random = new Random();
        foreach (string enemy in waves[Global.GetMap()][wave])
        {
            enemiesSpawned++;
            enemiesAlive++;
            Instantiate(enemyTypes[enemy], spawnPoint.position, spawnPoint.rotation);
        
            yield return new WaitForSeconds(randomDelay(0.5f, 3.5f));
        }
    }

    public void CheckWaveFinished()
    {
        if (enemiesAlive <= 0 && (!inWave || enemiesSpawned >= waves[Global.GetMap()][_waveNumber].Count))
        {
            inWave = false;
            if (waves[Global.GetMap()].Count < _waveNumber)
            {
                _win();
            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>().GetHud().ShowWaveSpawnText();
            }
            
        }
    }

    public bool CanStartNext()
    {
        return !inWave;
    }

    // generate a float between range
    private float randomDelay(float min, float max)
    {
        Random random = new Random();
        return (float)random.NextDouble() * (max - min) + min;
    }
    
    public void SetInWave(bool flag)
    {
        this.inWave = flag;
    }
    
    public void decreaseEnemiesAlive()
    {
        this.enemiesAlive--;
    }
}
