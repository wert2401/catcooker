using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameCondition
{
    NotStarted,
    Paused,
    Playing
}

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Condition = GameCondition.NotStarted;
        dataStore = new DataStore();
        dataStore.Init(new SaveModel() { Score=0, Ingridients = ingridientModels });
    }

    public GameCondition Condition { get; private set; }

    [SerializeField]
    private GameObject map;
    [SerializeField]
    private Navigation navigation;
    [SerializeField]
    private Score score;
    [SerializeField]
    private List<IngridientModel> ingridientModels;

    private IDataStore dataStore;

    public Action GameStarted { get; set; }
    public Action GameStopped { get; set; }
    public Action GamePaused { get; set; }
    public Action GameUnpaused { get; set; }
    public Action<IngridientModel> IngridientCollected { get; set; }
    public Action<RecipeModel> RecipeCollected { get; set; }
    public Action<RecipeModel> RecipeGenerated { get; set; }
    public Action RecipeTimeIsOver { get; set; }

    private void Start()
    {
        Load();
    }

    public List<IngridientModel> GetAvailibleIngridients()
    {
        return ingridientModels.Where(x => x.IsAvailable == true).ToList();
    }

    public void StartGame()
    {
        Condition = GameCondition.Playing;
        map.SetActive(true);

        GameStarted?.Invoke();
    }

    public void StopGame()
    {
        Condition = GameCondition.NotStarted;
        map.SetActive(false);

        GameStopped?.Invoke();

        UpdateIngridients();

        Save();

        navigation.GoTo("Menu Screen");
    }

    public void PauseGame()
    {
        Condition = GameCondition.Paused;
        map.SetActive(false);
        GamePaused?.Invoke();
    }

    public void UnpauseGame()
    {
        Condition = GameCondition.Playing;
        map.SetActive(true);
        GameUnpaused?.Invoke();
    }

    private void UpdateIngridients()
    {
        foreach (IngridientModel ingridient in ingridientModels)
        {
            if (ingridient.PointsNeeded <= score.MaxScore)
                ingridient.IsAvailable = true;
        }
    }

    private void Save()
    {
        SaveModel model = new SaveModel()
        {
            Ingridients = ingridientModels,
            Score = score.MaxScore
        };

        dataStore.Save(model);
    }

    private void Load()
    {
        SaveModel model = dataStore.Load();

        score.MaxScore = model.Score;
        ingridientModels = model.Ingridients;
    }
}
