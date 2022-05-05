using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GameStarted?.Invoke();
    }

    [SerializeField]
    private List<IngridientModel> IngridientModels;

    public Action GameStarted { get; set; }
    public Action GameStopped { get; set; }
    public Action<IngridientModel> IngridientCollected { get; set; }
    public Action<RecipeModel> RecipeCollected { get; set; }

    public List<IngridientModel> GetAvailibleIngridients()
    {
        return IngridientModels.Where(x => x.IsAvailable == true).ToList();
    }

}
