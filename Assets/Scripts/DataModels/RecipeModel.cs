using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeModel
{
    public List<IngridientModel> Ingridients { get; set; } = new List<IngridientModel>();
    public float Duration { get; set; }

    public int GetPoints()
    {
        int sumOfPoints = 0;
        foreach (var ingridient in Ingridients)
        {
            sumOfPoints += ingridient.GivenPoints;
        }
        return sumOfPoints;
    }
}
