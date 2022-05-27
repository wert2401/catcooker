using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private GameObject ingridientPrefab;
    [SerializeField]
    private RecipeHolder recipeHolder;
    [SerializeField]
    private Transform spawnedIngridients;

    private List<IngridientModel> ingridients;
    private List<IngridientModel> wrongIngredients;
    private float rnd;
    private DifficultyModel difficulty;

    void Start()
    {
        GameState.Instance.GameStarted += onGameStarted;
        GameState.Instance.GameStopped += onGameStopped;
        GameState.Instance.GamePaused += onGamePaused;
        GameState.Instance.GameUnpaused += onGameUnpaused;
        GameState.Instance.DifficultyChanged += onDifficultyChanged;
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
    private void onDifficultyChanged(DifficultyModel difficulty)
    {
        this.difficulty = difficulty;
    }

    private void updateIngridients()
    {
        ingridients = GameState.Instance.GetAvailibleIngridients();
        wrongIngredients = GameState.Instance.GetWrongIngredients();
    }

    IEnumerator spawn(float timeBeforeSpawn)
    {
        yield return new WaitForSeconds(timeBeforeSpawn);

        while (true)
        {
            Vector2 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), 0, 0));

            spawnPosition.y = spawnedIngridients.position.y;

            IngridientModel pickedIngredient = pickRandomIngredient();

            GameObject currentIngridientObject = Instantiate(ingridientPrefab, spawnPosition, new Quaternion(), spawnedIngridients);

            currentIngridientObject.GetComponent<IngridientObject>().Model = pickedIngredient;
            currentIngridientObject.GetComponent<IngridientObject>().SetRandomSpeed(difficulty.SpeedOfFalling);

            yield return new WaitForSeconds(difficulty.SpawnTime);
        }
    }

    IngridientModel pickRandomIngredient()
    {
        IngridientModel currentIngridientModel = ingridients[Random.Range(0, ingridients.Count)];

        rnd = Random.Range(0f, 1f);
        if (rnd < difficulty.SpawnChances.NeededIngredientChance && recipeHolder.CountOfRemainingIngridients > 0)
        {
            currentIngridientModel = recipeHolder.RemainingIngridients[Random.Range(0, recipeHolder.CountOfRemainingIngridients)];
        }
        else
        {
            rnd = Random.Range(0f, 1f);
            if (rnd < difficulty.SpawnChances.WrongIngredientChance)
            {
                currentIngridientModel = wrongIngredients[Random.Range(0, wrongIngredients.Count)];
            }
        }

        return currentIngridientModel;
    }
}
