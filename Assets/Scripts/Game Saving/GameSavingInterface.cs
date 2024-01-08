using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines an interface for game data loading and saving functionalities
public interface GameSavingInterface 
{
    // Declares a method to load game data from an external source
    void LoadGameData(GameData data);

    // Declares a method to save game data, with data passed by reference
    void SaveGameData(ref GameData data);

    
    /*
    Saving and Loading data in individual scripts instrucctions:

    Put GameSavingInterface after MonoBehaviour with a comma at the top of the script
    i.e MonoBehaviour, GameSavingInterface

    Copy and paste the below methods in order to save and load data
    use the appropriate variable for the appropriate script

    public void LoadGameData(GameData data)
    {
        this.variableName = data.variableName;
    }

    public void SaveGameData(ref GameData data)
    {
        data.variableName = this.variableName;   
    }

    */
}
