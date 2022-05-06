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
    private RecipeHolder recipeHolder;

    private List<IngridientModel> ingridients;
    private IEnumerator spawnCoroutine;

    void Start()
    {
        spawnCoroutine = spawn();
        //Need to fix
        //GameState.Instance.GameStarted += onGameStarted;
        GameState.Instance.GameStopped += onGameStopped;
        onGameStarted();
    }

    private void onGameStarted()
    {
        updateIngridients();
        StartCoroutine(spawnCoroutine);
    }
    private void onGameStopped()
    {
        StopCoroutine(spawnCoroutine);
    }

    private void updateIngridients()
    {
        ingridients = GameState.Instance.GetAvailibleIngridients();
    }

    IEnumerator spawn()
    {
        while (true)
        {
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 0, 0));

            spawnPosition.y = transform.position.y;

            IngridientModel currentIngridientModel;
            if (UnityEngine.Random.Range(0f, 1f) > 0.5f)
            {
                currentIngridientModel = ingridients[Random.Range(0, ingridients.Count)];
            }
            else
            {
                if (recipeHolder.CountOfRemainingIngridients == 0)
                    yield return new WaitForSeconds(0);
                currentIngridientModel = recipeHolder.RemainingIngridients[Random.Range(0, recipeHolder.CountOfRemainingIngridients)];
            }

            GameObject currentIngridientObject = Instantiate(ingridientPrefab, spawnPosition, new Quaternion(), transform);

            currentIngridientObject.GetComponent<IngridientObject>().Model = currentIngridientModel;

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
