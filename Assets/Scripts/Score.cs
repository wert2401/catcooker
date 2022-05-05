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
        GameState.Instance.RecipeCollected += OnRecipeCollected;
    }

    private void OnRecipeCollected(RecipeModel recipe)
    {
        score += recipe.GetPoints();
        scoreText.text = score.ToString();
    }
}
