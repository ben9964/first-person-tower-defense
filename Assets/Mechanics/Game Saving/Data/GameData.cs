using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marks the class as serializable for data storage or transmission.
[System.Serializable]
public class GameData 
{
    // variable for wave number
    public int waveNumber;

    // variable for player health
    public int playerHealth;

    // variable for base health
    public int baseHealth;

    // variable for player money
    public int playerMoney;

    // values defined in this method are the default values i.e when there is no game data to load
    public GameData()
    {
        this.waveNumber = 1;

        this.playerHealth = 100;

        this.baseHealth = 30;

        this.playerMoney = 100;
    }
    
}
