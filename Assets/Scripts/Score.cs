using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text maxScoreText;

    private int score = 0;
    private int maxScore;

    private void Start()
    {
        scoreText.text = score.ToString();
        GameState.Instance.RecipeCollected += onRecipeCollected;
        GameState.Instance.GameStopped += onGameStopped;
    }

    public int MaxScore
    {
        get
        {
            return maxScore;
        }
        set
        {
            maxScore = value;
            maxScoreText.text = maxScore.ToString();
        }
    }

    private void onRecipeCollected(RecipeModel recipe)
    {
        score += recipe.GetPoints();
        scoreText.text = score.ToString();
    }

    private void onGameStopped()
    {
        if (score > maxScore)
        {
            MaxScore = score;
        }

        score = 0;
        scoreText.text = score.ToString();
    }
}
