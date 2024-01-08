using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;     // Includes the System namespace for fundamental base classes and base interfaces
using System.IO;  // Includes the System.IO namespace for file and stream I/O (input/output) operations


// This class handles loading and saving game data to and from files in JSON format
public class FileDataHandler
{
   private string gamedataDirectoryPath = "";
   private string gamedataFileName = "";

    // Initialises the file handler with specified directory and file name for game data
   public FileDataHandler(string gamedataDirectoryPath, string gamedataFileName)
   {
        this.gamedataDirectoryPath = gamedataDirectoryPath;
        this.gamedataFileName = gamedataFileName;
   }

    // Loads game data from a file, returning a GameData object if successful, null otherwise
   public GameData Load()
   {
        string fullPath = Path.Combine(gamedataDirectoryPath, gamedataFileName);
        Debug.Log("Game data file path (Load): " + fullPath);

        GameData loadedGameData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string gameDataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                using (StreamReader reader = new StreamReader(stream))
                {
                    gameDataToLoad = reader.ReadToEnd();
                }

            loadedGameData = JsonUtility.FromJson<GameData>(gameDataToLoad);

            }
            catch (Exception e)
            {
              Debug.LogError("Error occured when trying to load saved game data : " + fullPath + "\n" + e);  
            }
        }
        return loadedGameData;
   }

    // Saves the provided GameData object to a file in JSON format
   public void Save(GameData data)
   {
        string fullPath = Path.Combine(gamedataDirectoryPath, gamedataFileName);
        Debug.Log("Game data file path (Save): " + fullPath);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string gameDataToSave = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(gameDataToSave);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save game data to file: " + fullPath + "\n" + e);
        }
   }
}
