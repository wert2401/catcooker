using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeCollector : MonoBehaviour
{
    [SerializeField]
    private RecipeHolder recipeHolder;

    void Start()
    {
        GameState.Instance.IngridientCollected += onIngridientCollected;
        GameState.Instance.GameStarted += onGameStarted;
    }

    private void onIngridientCollected(IngridientModel ingridient)
    {
        if (recipeHolder.TryCollectIngridient(ingridient))
        {
            if (recipeHolder.CountOfRemainingIngridients == 0)
            {
                GameState.Instance.RecipeCollected?.Invoke(recipeHolder.Model);
                recipeHolder.GenerateNewRecipe();
            }
        }
    }

    private void onGameStarted()
    {
        recipeHolder.GenerateNewRecipe();
    }
}
