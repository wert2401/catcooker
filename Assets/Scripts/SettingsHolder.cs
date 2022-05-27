using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHolder : MonoBehaviour
{
    private SettingsModel settings;
    public SettingsModel Settings
    {
        get
        {
            return settings;
        }
    }

    [SerializeField]
    private Slider sensivitySlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider effectsSlider;

    private void Start()
    {
        sensivitySlider.onValueChanged.AddListener(onSensivityChanged);
        musicSlider.onValueChanged.AddListener(onMusicChanged);
        effectsSlider.onValueChanged.AddListener(onEffectsChanged);
    }

    void onSensivityChanged(float value)
    {
        Settings.SensivityFactor = value;
        GameState.Instance.SettingsChanged?.Invoke(Settings);
    }

    void onMusicChanged(float value)
    {
        Settings.MusicVolume = value;
        GameState.Instance.SettingsChanged?.Invoke(Settings);
    }

    void onEffectsChanged(float value)
    {
        Settings.SoundVolume = value;
        GameState.Instance.SettingsChanged?.Invoke(Settings);
    }

    public void SetSettings(SettingsModel settings)
    {
        this.settings = settings;
        sensivitySlider.value = settings.SensivityFactor;
        musicSlider.value = settings.MusicVolume;
        effectsSlider.value = settings.SoundVolume;
        GameState.Instance.SettingsChanged?.Invoke(settings);
    }
}
