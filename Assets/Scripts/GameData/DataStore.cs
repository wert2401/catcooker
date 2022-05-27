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
        {
            if (IsSavedDataGood())
                return;
        }

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

    private bool IsSavedDataGood()
    {
        SaveModel sm = Load();
        if (sm == null)
            return false;

        foreach (IngridientModel item in sm.Ingridients)
        {
            if (item == null)
                return false;
        }

        if (sm.Settings == null)
            return false;

        return true;
    }

    private void removeSave()
    {
        File.Delete(pathToSave);
    }
}
