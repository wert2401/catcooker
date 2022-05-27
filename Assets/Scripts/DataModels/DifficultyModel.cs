using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/DifficultyLevel")]
public class DifficultyModel : ScriptableObject
{
    public float ScoreToIncrease;
    public float SpeedOfFalling;
    public float SpawnTime;
    public SpawnChances SpawnChances;
}
