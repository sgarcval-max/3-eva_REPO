using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        Debug.Log("MusicSlider: " + musicSlider);
        Debug.Log("SFXSlider: " + sfxSlider);

        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnMusicChanged(float value)
    {
        Debug.Log("Music volume cambiado a: " + value);
        AudioManager.instance.SetMusicVolume(value);
    }

    private void OnSFXChanged(float value)
    {
        Debug.Log("SFX volume cambiado a: " + value);
        AudioManager.instance.SetSFXVolume(value);
    }
}
