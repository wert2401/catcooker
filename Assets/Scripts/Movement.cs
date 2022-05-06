using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Touch touch;
    Vector2 offset = new Vector2();
    const float offsetFromZero = 1.8f;
    const float sensivity = 0.025f;
    float x = 0;

    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        //TouchControll();
        GyroControll();
    }

    void TouchControll()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);


            if (touch.phase == TouchPhase.Began)
            {
                offset = transform.position - Camera.main.ScreenToWorldPoint(touch.position);
            }


            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);

                float x = worldPosition.x + offset.x;

                x = Mathf.Clamp(x, -1.3f, 1.3f);

                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
    }

    void GyroControll()
    {
        x += -Input.gyro.rotationRate.z * sensivity;   
        x = Mathf.Clamp(x, -offsetFromZero, offsetFromZero);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
