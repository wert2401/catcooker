using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    private readonly List<GameObject> screens = new List<GameObject>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            screens.Add(transform.GetChild(i).gameObject);
        }
    }

    public void GoTo(string screenName)
    {
        bool isNavigated = false;
        foreach (GameObject screen in screens)
        {
            if (screenName != screen.name)
                screen.SetActive(false);
            else
            {
                screen.SetActive(true);
                isNavigated = true;
            }
        }

        if (!isNavigated)
            throw new System.Exception("Scene does not found!");
    }
}
