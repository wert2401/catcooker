using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHolder : MonoBehaviour
{
    [SerializeField]
    private AudioSource effects;
    [SerializeField]
    private AudioSource timer;
    [SerializeField]
    private AudioSource music;

    [Header("Music")]
    [SerializeField]
    private AudioClip mainTheme;
    [SerializeField]
    private List<AudioClip> musicOfDifficulties;

    [Header("Sound Effects")]
    [SerializeField]
    private AudioClip newRecordClip;
    [SerializeField]
    private AudioClip timerClip;
    [SerializeField]
    private AudioClip recipeCollectedClip;
    [SerializeField]
    private AudioClip healthReducedClip;
    [SerializeField]
    private AudioClip ingredientCollectedClip;
    [SerializeField]
    private AudioClip wrongIngredientCollectedClip;
    [SerializeField]
    private AudioClip meowClip;

    private void Start()
    {
        PlayMusic(mainTheme);

        GameState.Instance.SettingsChanged += onSettingsChanged;
        GameState.Instance.NewRecord += onNewRecord;
        GameState.Instance.TimerEnding += onTimerEnding;
        GameState.Instance.RecipeCollected += onRecipeCollected;
        GameState.Instance.HealthReduced += onHealthReduced;
        GameState.Instance.RightIngredientCollected += onRightIngredientCollected;
        GameState.Instance.WrongIngredientCollected += onWrongIngredientCollected;
        GameState.Instance.DifficultyChanged += onDifficultyChanged;
        GameState.Instance.GameStopped += onGameStopped;
    }

    public void PlayEffect(AudioClip clip, float volume = 1f)
    {
        effects.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip)
    {
        float time = music.time;
        music.Stop();
        music.clip = clip;
        music.time = time;
        music.Play();
    }

    IEnumerator playDelayedMusic(AudioClip clip)
    {
        yield return new WaitForSeconds(8f - music.time);
        music.Stop();
        music.clip = clip;
        music.Play();
    }

    public void Meow()
    {
        PlayEffect(meowClip);
    }

    private void onSettingsChanged(SettingsModel settings)
    {
        effects.volume = settings.SoundVolume;
        music.volume = settings.MusicVolume;
    }

    private void onNewRecord()
    {
        PlayEffect(newRecordClip);
    }

    private void onTimerEnding()
    {
        timer.Stop();
        timer.clip = timerClip;
        timer.Play();
    }

    private void onRecipeCollected(RecipeModel recipe)
    {
        PlayEffect(recipeCollectedClip);
        timer.Stop();
    }

    private void onHealthReduced()
    {
        PlayEffect(healthReducedClip);
    }

    private void onRightIngredientCollected()
    {
        PlayEffect(ingredientCollectedClip, 4);
    }

    private void onWrongIngredientCollected(IngridientModel ingridient)
    {
        PlayEffect(wrongIngredientCollectedClip);
    }

    private void onDifficultyChanged(DifficultyModel difficulty)
    {
        if (musicOfDifficulties.Count > GameState.Instance.GetCurrentDifficultyLevel())
        {
            AudioClip clipToPlay = musicOfDifficulties[GameState.Instance.GetCurrentDifficultyLevel()];

            //Change music after previous is over
            //if (GameState.Instance.GetCurrentDifficultyLevel() == 0)
            //    PlayMusic(clipToPlay);
            //else
            //    StartCoroutine(playDelayedMusic(clipToPlay));

            PlayMusic(clipToPlay);
        }
    }

    private void onGameStopped()
    {
        //StartCoroutine(playDelayedMusic(mainTheme));
        PlayMusic(mainTheme);
        timer.Stop();
    }

}
