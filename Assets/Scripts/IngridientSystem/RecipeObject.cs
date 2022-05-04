using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeObject : MonoBehaviour
{
    [SerializeField]
    private List<IngridientModel> ingridients;
    [SerializeField]
    private float timeRamaining = 10f;

    public void ShowRecipe()
    {
        throw new NotImplementedException();
    }

    public void GenerateNewRecipe()
    {
        //...

        ShowRecipe();
    }
}
