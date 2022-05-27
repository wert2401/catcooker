using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject lifePrefab;
    [SerializeField]
    private Transform lifeUI; 

    private int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        GameState.Instance.GameStarted += onGameStarted;
        GameState.Instance.RecipeTimeIsOver += onRecipeTimeIsOver;
        GameState.Instance.WrongIngredientCollected += onWrongIngridientCollected;
    }

    private void onGameStarted()
    {
        currentHealth = maxHealth;
        showLifes();
    }

    private void onRecipeTimeIsOver()
    {
        reduceLifes();
    }

    private void onWrongIngridientCollected(IngridientModel ingridient)
    {
        if (ingridient.isWrong)
            reduceLifes();
    }

    private void reduceLifes()
    {
        currentHealth--;
        showLifes();

        GameState.Instance.HealthReduced?.Invoke();

        if (currentHealth <= 0)
        {
            GameState.Instance.StopGame();
        }
    }

    private void showLifes()
    {
        foreach (Transform child in lifeUI)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(lifePrefab, lifeUI);
        }
    }
}
