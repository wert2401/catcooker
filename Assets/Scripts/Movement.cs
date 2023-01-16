using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Touch touch;
    private Vector3 mousePosition;

    Vector2 offset = new Vector2();
    const float offsetFromZero = 1.8f;
    const float sensivity = 0.05f;
    private float sensivityFactor = 0.5f;

    float x = 0;

    void Start()
    {
        Input.gyro.enabled = true;
        GameState.Instance.SettingsChanged += onSettingsChanged;
    }

    void Update()
    {
        //if (Input.touchSupported)
        //    TouchControll();
        //else
            MouseControll();

        //GyroControll();
    }

    private void MouseControll()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
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
        x += -Input.gyro.rotationRate.z * sensivity * sensivityFactor;
        x = Mathf.Clamp(x, -offsetFromZero, offsetFromZero);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    void onSettingsChanged(SettingsModel settings)
    {
        sensivityFactor = settings.SensivityFactor;
    }
}
