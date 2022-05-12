using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
        GameState.Instance.RecipeCollected += onRecipeCollected;
        //probably does not need anymore
        //GameState.Instance.RecipeTimeIsOver += onRecipeTimeIsOver;
    }

    private void onRecipeCollected(RecipeModel recipe)
    {
        score += recipe.GetPoints();
        scoreText.text = score.ToString();
    }

    private void onRecipeTimeIsOver()
    {
        score -= 100;
        scoreText.text = score.ToString();
    }
}
