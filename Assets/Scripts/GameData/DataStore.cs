using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataStore : IDataStore
{
    private string pathToSave = Application.persistentDataPath + "/data.json";

    public void Init(SaveModel firstSaveModel)
    {
        if (File.Exists(pathToSave))
            return;
        else
            Save(firstSaveModel);
    }

    public SaveModel Load()
    {
        string json = File.ReadAllText(pathToSave);
        SaveModel model = JsonUtility.FromJson<SaveModel>(json);
        return model;
    }

    public void Save(SaveModel saveModel)
    {
        string json = JsonUtility.ToJson(saveModel);

        File.WriteAllText(pathToSave, json);
    }

    private void removeSave()
    {
        File.Delete(pathToSave);
    }
}
