using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SaveSystem : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<ISaveSystem> saveSystemObjects;
    private FileHandler fileHandler;
    public static SaveSystem Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fileHandler = new FileHandler(Application.persistentDataPath, fileName);
        saveSystemObjects = FindAllSaveSystemObjects();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileHandler.Load();

        if (gameData == null)
        {
            NewGame();
        }

        foreach (ISaveSystem saveObj in saveSystemObjects)
        {
            saveObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (ISaveSystem saveObj in saveSystemObjects)
        {
            saveObj.SaveData(ref gameData);
        }

        fileHandler.Save(gameData);
    }

    private List<ISaveSystem> FindAllSaveSystemObjects()
    {
        IEnumerable<ISaveSystem> saveSystemObjects = FindObjectsByType<MonoBehaviour>().OfType<ISaveSystem>();
        return new List<ISaveSystem>(saveSystemObjects);
    }
    
}
