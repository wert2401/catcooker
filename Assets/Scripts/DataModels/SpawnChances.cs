using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty/SpawnChances")]

public class SpawnChances : ScriptableObject
{
    public float NeededIngredientChance;
    public float WrongIngredientChance;
}
