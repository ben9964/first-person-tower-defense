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
            this.gameData = new GameData();
        }

        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.LoadGameDate(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.SaveGameData(ref gameData);
        }

        fileDataHandler.Save(gameData);
        
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<GameSavingInterface> FindAllGameDataPersistenceObjects()
    {
        IEnumerable<GameSavingInterface> gamedataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<GameSavingInterface>();

        return new List<GameSavingInterface>(gamedataPersistenceObjects);
    }
    
}
