
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngridientCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameState.Instance.IngridientCollected?.Invoke(collision.GetComponent<IngridientObject>().Model);
        Destroy(collision.gameObject);
    }
}
