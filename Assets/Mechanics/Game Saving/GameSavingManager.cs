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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        LoadToGameData();
    }

    public void NewGameData()
    {
        this.gameData = new GameData();
        SaveGameData();
    }

    public void LoadToGameData()
    {
        this.gameData = fileDataHandler.Load();
    }

    public void LoadToGame()
    {
        foreach (GameSavingInterface gamedataPersistenceObj in FindAllGameDataPersistenceObjects())
        {
            gamedataPersistenceObj.LoadGameData(gameData);
        }
    }

    public void SaveGameData()
    {
        foreach (GameSavingInterface gamedataPersistenceObj in FindAllGameDataPersistenceObjects())
        {
            gamedataPersistenceObj.SaveGameData(ref gameData);
        }
        
        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private List<GameSavingInterface> FindAllGameDataPersistenceObjects()
    {
        IEnumerable<GameSavingInterface> gamedataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<GameSavingInterface>();

        return new List<GameSavingInterface>(gamedataPersistenceObjects);
    }

    //Checks if gamaData object is null
    public bool HasGameData()
    {
        return this.gameData != null;
    }

    public GameData GetGameData()
    {
        return this.gameData;
    }
}
