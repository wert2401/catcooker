using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IngridientObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;

    private IngridientModel model;
    public IngridientModel Model
    {
        get { return model; }
        set
        {
            model = value;
            transform.name = model.name;
            gameObject.GetComponent<SpriteRenderer>().sprite = model.Sprite;
        }
    }

    void FixedUpdate()
    {
        transform.position -= new Vector3(0, speed * Time.fixedDeltaTime, 0);

        Destroy(gameObject, 5f);
    }

    public void SetRandomSpeed(float speed)
    {
        this.speed = speed + Random.Range(-(speed * 0.4f), (speed * 0.4f));
    }
}
