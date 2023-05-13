using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage ConFig")]
    [SerializeField] private string fileName;
    [SerializeField] private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance {get;private set;}
    private void Start() {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjact();
        LoadGame();
    }
    private void Awake(){
        if (instance != null){
            Debug.LogError("Duplicate Data Persistence Manager");
        }
        instance = this;
    }

    public void NewGame(){
        var jsonFile = Resources.Load<TextAsset>("GameData");
        this.gameData = new GameData(jsonFile);
    }
    public void LoadGame(){
        // TODO === Load any saved data from file
        this.gameData = dataHandler.Load();
        // if no data -> new data
        if (this.gameData == null){
            Debug.Log("No data was found. Reset to default.");
            NewGame();
        }
        // TODO === Push loaded data to all other script
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }
    public void SaveGame(){
        // TODO === pass data to other script to update
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        // TODO === save data to file
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjact(){
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
