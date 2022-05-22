using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientUI : MonoBehaviour
{
    private IngridientModel ingr;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text pointsNeeded;
    [SerializeField]
    private Image sprite;
    [SerializeField]
    private Color openColor;
    [SerializeField]
    private Color closeColor;

    public void SetIngredientModel(IngridientModel ingrModel)
    {
        this.ingr = ingrModel;
        title.text = ingr.Name;
        pointsNeeded.text = ingr.PointsNeeded.ToString();
        sprite.sprite = ingrModel.Sprite;
        if (ingr.IsAvailable)
        {
            GetComponent<Image>().color = openColor;
        } else
        {
            GetComponent<Image>().color = closeColor;
        }
    }

}
