using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHolder : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float recipeDuration;
    private float remainingTime;
    private bool isThereARecipe = false;

    void Start()
    {
        GameState.Instance.RecipeGenerated += onRecipeGenerated;
    }

    void Update()
    {
        if (isThereARecipe)
        {
            remainingTime -= Time.deltaTime;
            slider.value = remainingTime / recipeDuration;
            if (remainingTime <= 0)
            {
                isThereARecipe = false;
                GameState.Instance.RecipeTimeIsOver?.Invoke();
            }
        }
    }

    private void onRecipeGenerated(RecipeModel recipe)
    {
        recipeDuration = recipe.Duration;
        remainingTime = recipe.Duration;
        isThereARecipe = true;
    }
}
