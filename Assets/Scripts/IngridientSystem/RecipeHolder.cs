using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeHolder : MonoBehaviour
{
    [SerializeField]
    private int secondsOnIngridient = 5;

    [HideInInspector]
    public RecipeModel Model { get; private set; } = new RecipeModel();
    [HideInInspector]
    public int CountOfRemainingIngridients
    {
        get
        {
            return remainingIngridients.Count;
        }
    }
    [HideInInspector]
    public List<IngridientModel> RemainingIngridients { get { return remainingIngridients; } }

    private List<IngridientModel> remainingIngridients = new List<IngridientModel>();

    [SerializeField]
    private int maxCountOfIndridients = 5;
    [SerializeField]
    private Transform recipeUI;
    [SerializeField]
    private GameObject ingridientUIPrefab;

    public RecipeModel GenerateNewRecipe(List<IngridientModel> ingridients)
    {
        Model.Ingridients.Clear();

        int countOfIngridientsInRecipe = UnityEngine.Random.Range(1, maxCountOfIndridients + 1);

        int randomIndex = 0;
        for (int i = 0; i < countOfIngridientsInRecipe; i++)
        {
            randomIndex = UnityEngine.Random.Range(0, ingridients.Count);
            Model.Ingridients.Add(ingridients[randomIndex]);
            remainingIngridients.Add(ingridients[randomIndex]);
        }

        Model.Duration = countOfIngridientsInRecipe * secondsOnIngridient;

        ShowRecipe();

        return Model;
    }
    public bool TryCollectIngridient(IngridientModel ingridient)
    {
        if (remainingIngridients.Contains(ingridient))
        {
            remainingIngridients.Remove(ingridient);
            ShowRecipe();
            return true;
        }
        else
            return false;
    }
    private void ShowRecipe()
    {
        foreach (Transform child in recipeUI.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (IngridientModel ingridient in remainingIngridients)
        {
            GameObject ingridientUIObject = Instantiate(ingridientUIPrefab, recipeUI);
            ingridientUIObject.GetComponent<Image>().sprite = ingridient.Sprite;
        }
    }
}
