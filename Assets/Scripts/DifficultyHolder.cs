using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHolder : MonoBehaviour
{
    [SerializeField]
    private List<DifficultyModel> difficulties;

    private int currentDifficulty = 0;

    public DifficultyModel Difficulty { get; private set; }
    public int CurrentDifficultyLevel { get { return currentDifficulty; } }

    public void ResetDifficulty()
    {
        currentDifficulty = 0;
        Difficulty = difficulties[currentDifficulty];
        GameState.Instance.DifficultyChanged?.Invoke(Difficulty);
    }

    public void CheckDifficultyIncreasing(int score)
    {
        if (score > Difficulty.ScoreToIncrease)
            increaseDifficulty();
    }

    private void increaseDifficulty()
    {
        if (currentDifficulty < difficulties.Count-1)
        {
            currentDifficulty++;
            Difficulty = difficulties[currentDifficulty];
            GameState.Instance.DifficultyChanged?.Invoke(Difficulty);
        }
    }
}
