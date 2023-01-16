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
        //dataStore = new DataStore();
        //dataStore.Init(new SaveModel()
        //{
        //    Score = 0,
        //    Ingridients = ingridientModels,
        //    Settings = new SettingsModel()
        //    {
        //        MusicVolume = 0.5f,
        //        SensivityFactor = 0.5f,
        //        SoundVolume = 0.5f
        //    }
        //});
    }

    public GameCondition Condition { get; private set; }

    [Header("Holders")]
    [SerializeField]
    private GameObject map;
    [SerializeField]
    private Navigation navigation;
    [SerializeField]
    private Score score;
    [SerializeField]
    private SettingsHolder settingsHolder;
    [SerializeField]
    private DifficultyHolder difficultyHolder;

    [Header("Ingredients")]
    [SerializeField]
    private List<IngridientModel> ingridientModels;
    [SerializeField]
    private List<IngridientModel> wrongIngridientModels;

    //private IDataStore dataStore;

    public Action GameStarted { get; set; }
    public Action GameStopped { get; set; }
    public Action GamePaused { get; set; }
    public Action GameUnpaused { get; set; }
    public Action<IngridientModel> IngridientCollected { get; set; }
    public Action<RecipeModel> RecipeCollected { get; set; }
    public Action<RecipeModel> RecipeGenerated { get; set; }
    public Action RecipeTimeIsOver { get; set; }
    public Action<SettingsModel> SettingsChanged { get; set; }
    public Action NewRecord { get; set; }
    public Action TimerEnding { get; set; }
    public Action<IngridientModel> WrongIngredientCollected { get; set; }
    public Action RightIngredientCollected { get; set; }
    public Action HealthReduced { get; set; }
    public Action<DifficultyModel> DifficultyChanged { get; set; } 

    private void Start()
    {
        Load();
        map.SetActive(false);
    }

    public List<IngridientModel> GetAvailibleIngridients()
    {
        return ingridientModels.Where(x => x.IsAvailable == true).ToList();
    }
    public List<IngridientModel> GetIngredients()
    {
        return ingridientModels;
    }
    public List<IngridientModel> GetWrongIngredients()
    {
        return wrongIngridientModels;
    }
    public int GetCurrentDifficultyLevel()
    {
        return difficultyHolder.CurrentDifficultyLevel;
    }

    public void StartGame()
    {
        Condition = GameCondition.Playing;
        map.SetActive(true);
        difficultyHolder.ResetDifficulty();

        GameStarted?.Invoke();
    }

    public void StopGame()
    {
        Condition = GameCondition.NotStarted;
        map.SetActive(false);

        GameStopped?.Invoke();

        updateIngridients();

        //Save();

        navigation.GoTo("Menu Screen");
    }

    public void PauseGame()
    {
        if (Condition == GameCondition.Playing)
        {
            Condition = GameCondition.Paused;
            map.SetActive(false);
            GamePaused?.Invoke();
        }
    }

    public void UnpauseGame()
    {
        if (Condition == GameCondition.Paused)
        {
            Condition = GameCondition.Playing;
            map.SetActive(true);
            GameUnpaused?.Invoke();
        }
    }

    public void CheckDifficultyIncreasing(int score)
    {
        difficultyHolder.CheckDifficultyIncreasing(score);
    }

    private void updateIngridients()
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
            Score = score.MaxScore,
            Settings = settingsHolder.Settings
        };

        //dataStore.Save(model);
    }

    private void Load()
    {
        //SaveModel model = dataStore.Load();

        //score.MaxScore = model.Score;
        //ingridientModels = model.Ingridients;
        //settingsHolder.SetSettings(model.Settings);

        updateIngridients();
    }
}
