using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingridient : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.01f;

    void FixedUpdate()
    {
        transform.position -= new Vector3(0, speed*Time.fixedDeltaTime, 0);    

        Destroy(gameObject, 5f);
    }
}
