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
        GameState.Instance.WrongIngredientCollected += onWrongIngridientCollected;
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

        GameState.Instance.CheckDifficultyIncreasing(score);
    }

    private void onWrongIngridientCollected(IngridientModel ingridient)
    {
        score -= ingridient.GivenPoints * 2;
        if (score < 0)
            score = 0;
        scoreText.text = score.ToString();
    }

    private void onGameStopped()
    {
        if (score > maxScore)
        {
            MaxScore = score;
            GameState.Instance.NewRecord?.Invoke();
        }

        score = 0;
        scoreText.text = score.ToString();
    }
}
