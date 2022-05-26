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

    private void Start()
    {
        sensivitySlider.onValueChanged.AddListener(onSensivityChanged);
    }

    void onSensivityChanged(float value)
    {
        Settings.SensivityFactor = value;
        GameState.Instance.SettingsChanged?.Invoke(Settings);
    }

    public void SetSettings(SettingsModel settings)
    {
        this.settings = settings;
        sensivitySlider.value = settings.SensivityFactor;
        GameState.Instance.SettingsChanged?.Invoke(settings);
    }
}
