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

public class WaveSpawner : MonoBehaviour
{
    [SerializedDictionary("Enemy Name", "Enemy Prefab")]
    public SerializedDictionary<String, GameObject> enemyTypes;

    // Reference to the location where enemies will be spawned.
    public Transform spawnPoint;

    // The number of the current wave.
    private int _waveNumber = 1;

    // UI text elements to display wave info to the player.
    public TextMeshProUGUI waveText;

    private bool inWave = false;

    private Dictionary<Int32, List<String>> waves = new(){
            {1, new List<String>{"regular", "regular", "regular", "slow"}},
            {2, new List<String>{"slow", "regular", "slow", "regular", "fast", "fast"}},
            {3, new List<String>{"fast", "fast", "fast", "fast", "fast"}},
            {4, new List<String>{"fast", "fast", "slow", "slow", "fast", "fast", "regular", "fast", "fast", "fast"}},
            {5, new List<String>{"fast", "fast", "regular", "slow", "slow", "slow", "slow"}}
    }; 

    // Called when the game starts.
    void Start()
    {
        // Initial setup can be done here if needed.
        spawnPoint = GameObject.FindWithTag("Start").transform;
    }

    // Function to handle the spawning of a wave of enemies.
    public void NextWave()
    {
        inWave = true;
        waveText.text = "Wave Number: " + _waveNumber + "/" + waves.Count;
        SpawnEnemies(_waveNumber); 
        _waveNumber++;  // Increase the wave number for the next wave.
    }

    private void _win()
    {
        AbstractPlayer player = GameObject.FindWithTag("Player").GetComponent<AbstractPlayer>();
        player.GetHud().SendMessage("You Win!", new Color32(0, 255, 0, 255));
        player.Freeze();
    }

    // Function to spawn a single enemy.
    void SpawnEnemies(int wave)
    {
        // instantiate each enemy in this wave
        foreach (string enemy in waves[wave])
        {
            this.Invoke(() =>
            {
                Instantiate(enemyTypes[enemy], spawnPoint.position, spawnPoint.rotation);
            }, randomDelay(0.2f, 2.5f));
        }
    }

    public void CheckWaveFinished(bool enemyDeath)
    {
        int enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        bool isFinished = enemyDeath ? enemies <= 1 : enemies <= 0;
        if (isFinished)
        {
            inWave = false;
            if (waves.Count < _waveNumber)
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
}
