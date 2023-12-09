using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marks the class as serializable for data storage or transmission.
[System.Serializable]
public class GameData 
{
    public int waveNumber;
    public float playerHealth;
    public float baseHealth;
    public float playerMoney;

    public int playerLevel;
    public float playerXp;

    // This class stores essential game data
    // values defined in this method are the default values i.e when there is no game data to load
    public GameData()
    {
        this.waveNumber = 0;

        this.playerHealth = 100;

        this.baseHealth = 30;

        this.playerMoney = 100;

        this.playerLevel = 1;

        this.playerXp = 0;
    }
    
}
