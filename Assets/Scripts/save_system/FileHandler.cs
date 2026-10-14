using UnityEngine;
using System;
using System.IO;

public class FileHandler
{
    private string dataFilePath = "";
    private string dataFileName = "";
    public FileHandler(string dataFilePath, string dataFileName)
    {
        this.dataFilePath = dataFilePath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataFilePath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                Debug.Log(dataToLoad);

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

                Debug.Log(loadedData.ToString());

            } catch (Exception error)
            {
                Debug.LogError("Error occured when trying to load at " + fullPath + ": " + error);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataFilePath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        } catch (Exception error)
        {
            Debug.LogError("Error occured when trying to save at " + fullPath + ": " + error);
        }
    }
}
