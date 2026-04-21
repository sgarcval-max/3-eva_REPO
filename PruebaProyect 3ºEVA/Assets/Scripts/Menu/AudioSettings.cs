using UnityEngine;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Cargar valores guardados
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolumes();

        // Escuchar cambios en los sliders
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    private void OnMusicChanged(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
        ApplyVolumes();
    }

    private void OnSFXChanged(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
        AudioManager.instance.SetSFXVolume(sfxSlider.value);
    }
}
