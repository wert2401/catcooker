using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime = 2f;
    [SerializeField]
    private GameObject ingridientPrefab;
    [SerializeField]
    private List<IngridientModel> ingridients;

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

            IngridientModel currentIngridientModel = ingridients[Random.Range(0, ingridients.Count - 1)];

            GameObject currentIngridientObject = Instantiate(ingridientPrefab, spawnPosition, new Quaternion(), transform);

            currentIngridientObject.GetComponent<IngridientObject>().SetModel(currentIngridientModel);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
