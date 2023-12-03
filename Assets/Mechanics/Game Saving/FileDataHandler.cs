using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
   private string gamedataDirectoryPath = "";
   private string gamedataFileName = "";

   public FileDataHandler(string gamedataDirectoryPath, string gamedataFileName)
   {
        this.gamedataDirectoryPath = gamedataDirectoryPath;
        this.gamedataFileName = gamedataFileName;
   }

   public GameData Load()
   {
        string fullPath = Path.Combine(gamedataDirectoryPath, gamedataFileName);
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

   public void Save(GameData data)
   {
        string fullPath = Path.Combine(gamedataDirectoryPath, gamedataFileName);
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
