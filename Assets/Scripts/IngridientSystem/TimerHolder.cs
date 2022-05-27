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
    private bool timerSoundStarted = false;

    void Start()
    {
        GameState.Instance.RecipeGenerated += onRecipeGenerated;
    }

    void Update()
    {
        if (isThereARecipe && GameState.Instance.Condition == GameCondition.Playing)
        {
            remainingTime -= Time.deltaTime;
            slider.value = remainingTime / recipeDuration;
            if (remainingTime <= 0)
            {
                isThereARecipe = false;
                GameState.Instance.RecipeTimeIsOver?.Invoke();
            }
            if (remainingTime < 4 && !timerSoundStarted)
            {
                GameState.Instance.TimerEnding?.Invoke();
                timerSoundStarted = true;
            }
        }
    }

    private void onRecipeGenerated(RecipeModel recipe)
    {
        recipeDuration = recipe.Duration;
        remainingTime = recipe.Duration;
        isThereARecipe = true;
        timerSoundStarted = false;
    }
}
