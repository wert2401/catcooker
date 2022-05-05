using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/New recipe")]
public class IngridientModel : ScriptableObject
{
    public string Name;
    public int PointsNeeded;
    public int GivenPoints;
    public bool IsAvailable;
    public Sprite Sprite;
}
