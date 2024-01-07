using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Marks the class as serializable for data storage or transmission.
[System.Serializable]
public class GameData 
{
    public float playerXp;
    public int playerLevel;

    // values defined in this method are the default values i.e when there is no game data to load
    public GameData()
    {
        this.playerXp = 0;

        this.playerLevel = 1;
    }
    
    public float GetXp()
    {
        return playerXp;
    }
    
    public int GetLevel()
    {
        return playerLevel;
    }
}
    
