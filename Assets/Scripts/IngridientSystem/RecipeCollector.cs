using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeCollector : MonoBehaviour
{
    [SerializeField]
    private RecipeHolder recipeHolder;

    void Start()
    {
        GameState.Instance.GameStarted += onGameStarted;
        GameState.Instance.IngridientCollected += onIngridientCollected;
        GameState.Instance.RecipeTimeIsOver += onRecipeTimeIsOver;
    }

    private void generateNewRecipe()
    {
        RecipeModel recipe = recipeHolder.GenerateNewRecipe(GameState.Instance.GetAvailibleIngridients());
        GameState.Instance.RecipeGenerated?.Invoke(recipe);
    }

    private void onGameStarted()
    {
        generateNewRecipe();
    }

    private void onIngridientCollected(IngridientModel ingridient)
    {
        if (recipeHolder.TryCollectIngridient(ingridient))
        {
            if (recipeHolder.CountOfRemainingIngridients == 0)
            {
                GameState.Instance.RecipeCollected?.Invoke(recipeHolder.Model);
                generateNewRecipe();
            }
        }
    }

    private void onRecipeTimeIsOver()
    {
        generateNewRecipe();
    }
}
