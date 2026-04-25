using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Musica por escena")]
    public AudioClip mainMenuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;

    [Header("Efectos de sonido")]
    public AudioClip footstepSFX;
    public AudioClip buttonSFX;
    public AudioClip deathSFX;
    public AudioClip respawnSFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cargar volumenes guardados
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Cambiar musica segun escena
        switch (scene.name)
        {
            case "MainMenu":
                PlayMusic(mainMenuMusic);
                break;
            case "Level 1":
                PlayMusic(level1Music);
                break;
            case "Level 2":
                PlayMusic(level2Music);
                break;
            case "Level 3":
                PlayMusic(level3Music);
                break;
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip) return;  // No reiniciar si es la misma musica
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Metodos publicos para efectos
    public void PlayFootstep()
    {
        if (footstepSFX != null)
            sfxSource.PlayOneShot(footstepSFX);
    }

    public void PlayButton()
    {
        if (buttonSFX != null)
            sfxSource.PlayOneShot(buttonSFX);
    }

    public void PlayDeath()
    {
        if (deathSFX != null)
            sfxSource.PlayOneShot(deathSFX);
    }

    public void PlayRespawn()
    {
        if (respawnSFX != null)
            sfxSource.PlayOneShot(respawnSFX);
    }

    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
