// This script manages the spawning of waves of enemies in the game.
// It also updates relevant UI elements to inform the player about the wave status.

using System.Collections;
using System.Collections.Generic;
using TMPro;  // Importing the namespace for TextMeshPro, which is used for UI text elements.
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // Reference to the enemy prefab that will be spawned.
    public Transform Enemy;

    // Reference to the location where enemies will be spawned.
    public Transform SpawnPoint;

    // Time delay between subsequent waves of enemies.
    public float TimeBetweenWaves = 6f;

    // Countdown timer to track time until the next wave.
    private float countdown = 3f;

    // The number of the current wave.
    private int WaveNumber = 1;

    // UI text elements to display wave info to the player.
    public TextMeshProUGUI waveNumber;  
    public TextMeshProUGUI waveCountdown;  
    public TextMeshProUGUI timeBetweenWaves; 

    // Called when the game starts.
    void Start()
    {
        // Initial setup can be done here if needed.
        SpawnPoint = GameObject.FindWithTag("Start").transform;
    }

    // Called every frame.
    void Update()
    {
        // Check if the countdown has reached zero, indicating it's time for a new wave.
        if (countdown <= 0f)
        {
            WaveSpawn();  // Spawn the wave of enemies.
            countdown = TimeBetweenWaves;  // Reset the countdown.
        }

        // Reduce the countdown by the time that has passed since the last frame.
        countdown -= Time.deltaTime;

        // Update the UI elements with current wave information.
        waveNumber.text = "Wave Number: " + WaveNumber;
        waveCountdown.text = "Wave Countdown: " + Mathf.Round(countdown).ToString();
        timeBetweenWaves.text = "Time Between Waves: " + TimeBetweenWaves.ToString();
    }

    // Function to handle the spawning of a wave of enemies.
    void WaveSpawn()
    {
        Debug.Log("Incoming Wave");  // Log a message for debugging purposes.

        // Spawn enemies equal to the current wave number.
        for (int i = 0; i < WaveNumber; i++)
        {
            SpawnEnemy();  // Call the function to spawn an individual enemy.
        }
        WaveNumber++;  // Increase the wave number for the next wave.
    }

    // Function to spawn a single enemy.
    void SpawnEnemy()
    {
        // Create (instantiate) an enemy at the designated spawn point.
        Instantiate(Enemy, SpawnPoint.position, SpawnPoint.rotation);
    }
}
