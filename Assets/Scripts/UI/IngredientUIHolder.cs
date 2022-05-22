using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientUIHolder : MonoBehaviour
{
    [SerializeField]
    private Transform panel;
    [SerializeField]
    private GameObject ingredientUIPrefab;

    public void UpdateIngredients()
    {
        clearPanel();

        foreach (IngridientModel ingridient in GameState.Instance.GetIngredients())
        {
            GameObject ingrUigo = Instantiate(ingredientUIPrefab, panel);
            ingrUigo.GetComponent<IngredientUI>().SetIngredientModel(ingridient);
        }
    }

    private void clearPanel()
    {
        foreach (Transform item in panel)
        {
            Destroy(item.gameObject);
        }
    }
}
