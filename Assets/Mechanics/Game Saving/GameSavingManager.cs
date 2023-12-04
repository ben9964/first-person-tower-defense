using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameSavingManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<GameSavingInterface> gamedataPersistenceObjects;
    private FileDataHandler fileDataHandler;
    public static GameSavingManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameSavingManger in scene");
        }
        instance = this;
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.gamedataPersistenceObjects = FindAllGameDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No saved game data found, all values set to defaults");
            this.gameData = new GameData();
        }

        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.LoadGameDate(gameData);
        }

        Debug.Log("Loaded Wave Number = " + gameData.waveNumber);
        Debug.Log("Loaded Player Health = " + gameData.playerHealth);
        Debug.Log("Loaded Base Health = " + gameData.baseHealth);
        Debug.Log("Loaded Player Money = " + gameData.playerMoney);
    }

    public void SaveGame()
    {
        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.SaveGameData(ref gameData);
        }

        Debug.Log("Saved Wave Number = " + gameData.waveNumber);
        Debug.Log("Saved Player Health = " + gameData.playerHealth);
        Debug.Log("Saved Base Health = " + gameData.baseHealth);
        Debug.Log("Saved Player Money = " + gameData.playerMoney);

        fileDataHandler.Save(gameData);
        
    }

    private void OnGameQuit()
    {
        SaveGame();
    }

    private List<GameSavingInterface> FindAllGameDataPersistenceObjects()
    {
        IEnumerable<GameSavingInterface> gamedataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<GameSavingInterface>();

        return new List<GameSavingInterface>(gamedataPersistenceObjects);
    }
    
}
