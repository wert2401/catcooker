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
    [SerializeField]
    private Transform spawnedIngridients;

    private List<IngridientModel> ingridients;

    void Start()
    {
        GameState.Instance.GameStarted += onGameStarted;
        GameState.Instance.GameStopped += onGameStopped;
        GameState.Instance.GamePaused += onGamePaused;
        GameState.Instance.GameUnpaused += onGameUnpaused;
    }

    private void onGameUnpaused()
    {
        StartCoroutine(spawn(1));
    }

    private void onGamePaused()
    {
        StopAllCoroutines();
    }

    private void onGameStarted()
    {
        updateIngridients();
        StartCoroutine(spawn(1));
    }
    private void onGameStopped()
    {
        StopAllCoroutines();
        foreach (Transform child in spawnedIngridients)
        {
            Destroy(child.gameObject);
        }
    }

    private void updateIngridients()
    {
        ingridients = GameState.Instance.GetAvailibleIngridients();
    }

    IEnumerator spawn(float timeBeforeSpawn)
    {
        yield return new WaitForSeconds(timeBeforeSpawn);

        while (true)
        {
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 0, 0));

            spawnPosition.y = spawnedIngridients.position.y;

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

            GameObject currentIngridientObject = Instantiate(ingridientPrefab, spawnPosition, new Quaternion(), spawnedIngridients);

            currentIngridientObject.GetComponent<IngridientObject>().Model = currentIngridientModel;

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
