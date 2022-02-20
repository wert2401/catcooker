using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngridientCollector : MonoBehaviour
{
    public Action IngridientCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        IngridientCollected?.Invoke();
    }
}
