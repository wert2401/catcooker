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
    }

    public GameCondition Condition { get; private set; }

    [SerializeField]
    private GameObject map;
    [SerializeField]
    private Navigation navigation;
    [SerializeField]
    private List<IngridientModel> ingridientModels;

    public Action GameStarted { get; set; }
    public Action GameStopped { get; set; }
    public Action<IngridientModel> IngridientCollected { get; set; }
    public Action<RecipeModel> RecipeCollected { get; set; }
    public Action<RecipeModel> RecipeGenerated { get; set; }
    public Action RecipeTimeIsOver { get; set; }

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

        navigation.GoTo("Menu Screen");

        GameStopped?.Invoke();
    }

    public void PauseGame()
    {
        Condition = GameCondition.Paused;
        map.SetActive(false);
    }

    public void UnpauseGame()
    {
        if (Condition == GameCondition.Paused)
        {
            Condition = GameCondition.Playing;
            map.SetActive(true);
        }
    }
}
