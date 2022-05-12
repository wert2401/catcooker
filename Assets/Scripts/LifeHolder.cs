using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject lifePrefab;

    private int maxHealth = 3;
    private int currentHealth;

    private void Start()
    {
        GameState.Instance.GameStarted += onGameStarted;
        GameState.Instance.RecipeTimeIsOver += onRecipeTimeIsOver;
    }

    private void onGameStarted()
    {
        currentHealth = maxHealth;
        showLifes();
    }

    private void onRecipeTimeIsOver()
    {
        currentHealth--;
        showLifes();

        if (currentHealth <= 0)
        {
            GameState.Instance.GameStopped?.Invoke();
        }
    }

    private void showLifes()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(lifePrefab, transform);
        }
    }
}
