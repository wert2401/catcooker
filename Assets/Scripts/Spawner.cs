using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> ingridients;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 0, 0));

            spawnPosition.y = transform.position.y;

            GameObject ingr = ingridients[Random.Range(0, ingridients.Count - 1)];

            Instantiate(ingr, spawnPosition, new Quaternion(), transform);

            yield return new WaitForSeconds(2f);
        }
    }
}
