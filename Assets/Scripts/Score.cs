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
    private IngridientCollector collector;

    private int score = 0;

    private void Start()
    {
        scoreText.text = score.ToString();
        collector.IngridientCollected += OnIngridientCollected;
    }

    private void OnIngridientCollected()
    {
        score += 1;
        scoreText.text = score.ToString();
    }
}
