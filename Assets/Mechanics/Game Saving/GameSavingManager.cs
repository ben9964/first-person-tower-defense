using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

// GameSavingManager manages the game's data saving and loading process, handling interactions with persistent storage
public class GameSavingManager : MonoBehaviour
{
    // Attribute to organize and label the gameData field in the Unity Editor
    [Header("Developing")]
    // Attribute to organize and label the gameData field in the Unity Editor
    [SerializeField] private bool initializeGameDataIfNull = false;

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
            Debug.LogError("More than one GameSavingManger in scene, Destorying the newest GameSavingManger");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    // OnEnable is called when the object becomes enabled and active, registering scene load and unload events
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // OnDisable is called when the object becomes disabled or inactive, unregistering scene load and unload events
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // OnSceneLoaded is called each time a scene is loaded, initializing game data persistence objects and loading game data
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded was Called");
        this.gamedataPersistenceObjects = FindAllGameDataPersistenceObjects();
        LoadGame();
    }

    // OnSceneUnloaded is called each time a scene is unloaded, triggering a save of the current game data
    public void OnSceneUnloaded(Scene scene)
    {    
        Debug.Log("OnSceneUnloaded was Called");
        SaveGame();   
    }

    // Initialises a new gameData object for a new game session
    public void NewGame()
    {
        this.gameData = new GameData();
        
        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.LoadGameDate(gameData);
        }
        SaveGame();
    }

    // Loads game data from the file and updates all gamedataPersistenceObjects with the loaded data
    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        // if there is not gameData and the initializeGameDataIfNull is checked then start a new game
        if (this.gameData == null && initializeGameDataIfNull)
        {
            NewGame();
        }

        if (this.gameData == null)
        {
            Debug.Log("No game data was found. A new game must be made first");
            return;
        }

        foreach (GameSavingInterface gamedataPersistenceObj in gamedataPersistenceObjects)
        {
            gamedataPersistenceObj.LoadGameDate(gameData);
        }
    }

    // Saves the current game data through all gamedataPersistenceObjects and then to the file
    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No game data was found. A new game must be made first");
            return;
        }

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

    //Checks if gamaData object is null
    public bool HasGameData()
    {
        return gameData != null;
    }
    
}
