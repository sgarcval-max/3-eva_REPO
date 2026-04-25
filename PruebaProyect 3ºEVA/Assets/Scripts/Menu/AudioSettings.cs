using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Textos de porcentaje")]
    public TextMeshProUGUI musicPercentText;
    public TextMeshProUGUI sfxPercentText;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Mostrar porcentaje inicial
        UpdateMusicText(musicSlider.value);
        UpdateSFXText(sfxSlider.value);

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnMusicChanged(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
        UpdateMusicText(value);
    }

    private void OnSFXChanged(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
        UpdateSFXText(value);
    }

    private void UpdateMusicText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        musicPercentText.text = percent + "%";
    }

    private void UpdateSFXText(float value)
    {
        int percent = Mathf.RoundToInt(value * 100);
        sfxPercentText.text = percent + "%";
    }
}
