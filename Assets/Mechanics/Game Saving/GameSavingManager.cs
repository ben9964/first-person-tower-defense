using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// GameSavingManager manages the game's data saving and loading process, handling interactions with persistent storage
public class GameSavingManager : MonoBehaviour
{
    // Attribute to organize and label the fileName field in the Unity Editor
    [Header("File Storage Config")]
    // Private serialized field for storing the file name, editable in the Unity Editor
    [SerializeField] private string fileName;
    // Private variable to hold game data 
    private GameData gameData;
    // Private list to hold objects that implement the GameSavingInterface
    private List<GameSavingInterface> gamedataPersistenceObjects;
    // Private variable for handling file data operations like load and save
    private FileDataHandler fileDataHandler;
    // Private variable for handling file data operations like load and save
    public static GameSavingManager instance { get; private set; }

    // Ensures only one instance of GameSavingManager exists and assigns it to 'instance'
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameSavingManger in scene");
        }
        instance = this;
    }

    // Initialises fileDataHandler and finds all objects that need game data persistence on game start
    private void Start()
    {
        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.gamedataPersistenceObjects = FindAllGameDataPersistenceObjects();
        LoadGame();
    }

    // Initialises a new gameData object for a new game session.
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    // Loads game data from the file and updates all gamedataPersistenceObjects with the loaded data
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

    // Saves the current game data through all gamedataPersistenceObjects and then to the file
    public void SaveGame()
    {
        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.SaveGameData(ref gameData);
        }

        fileDataHandler.Save(gameData);
        
    }

    // Automatically saves the game data when the application is about to quit
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    // Finds and returns a list of all objects in the scene that implement GameSavingInterface
    private List<GameSavingInterface> FindAllGameDataPersistenceObjects()
    {
        IEnumerable<GameSavingInterface> gamedataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<GameSavingInterface>();

        return new List<GameSavingInterface>(gamedataPersistenceObjects);
    }
    
}
