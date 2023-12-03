using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameSavingInterface 
{

    void LoadGameDate(GameData data);

    void SavedGameData(ref GameData data);

    
    /*
    Saving and Loading data in individual scripts instrucctions:

    Put GameSavingInterface after MonoBehaviour with a comma at the top of the script
    i.e , GameSavingInterface

    Copy and paste the below methods in order to save and load data
    use the appopriate variable for the appopriate script

    public void LoadGameData(GameData data)
    {
        this.waveNumber = data.waveNumber;

        this.playerHealth = data.playerHealth;

        this.baseHealth = data.baseHealth;

        this.playerMoney = data.playerMoney;

    }

    public void SaveGameData(ref GameData data)
    {
        data.waveNumber = this.waveNumber;

        data.playerHealth = this.playerHealth;

        data.baseHealth = this.baseHealth;

        data.playerMoney = this.playerMoney;
        
    }

    */
}
