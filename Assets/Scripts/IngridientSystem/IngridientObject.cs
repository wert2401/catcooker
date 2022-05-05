using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IngridientObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;

    private IngridientModel model;
    public IngridientModel Model
    {
        get { return model; }
        set
        {
            model = value;
            transform.name = model.Name;
            gameObject.GetComponent<SpriteRenderer>().sprite = model.Sprite;
        }
    }

    void FixedUpdate()
    {
        transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);

        Destroy(gameObject, 5f);
    }
}
