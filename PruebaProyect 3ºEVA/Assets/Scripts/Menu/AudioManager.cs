using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    [Header("Musica de paneles")]
    public AudioClip levelCompleteMusic;
    public AudioClip gameCompleteMusic;
    public AudioClip gameOverMusic;

    [Header("Efectos de sonido")]
    public AudioClip footstepSFX;
    public AudioClip buttonClickSFX;
    public AudioClip buttonHoverSFX;
    public AudioClip deathSFX;
    public AudioClip respawnSFX;
    public AudioClip jumpPlayer1SFX;
    public AudioClip jumpPlayer2SFX;
    public AudioClip polarityPositiveSFX;
    public AudioClip polarityNegativeSFX;

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
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);

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
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayLevelComplete()
    {
        PlayPanelMusic(levelCompleteMusic, 0.3f);
    }

    public void PlayGameComplete()
    {
        PlayPanelMusic(gameCompleteMusic, 0.3f);
    }

    public void PlayGameOver()
    {
        PlayPanelMusic(gameOverMusic, 0.3f);
    }

    private void PlayPanelMusic(AudioClip clip, float fadeDuration = 1f)
    {
        StartCoroutine(FadeOutAndPlay(clip, fadeDuration));
    }

    private IEnumerator FadeOutAndPlay(AudioClip clip, float fadeDuration)
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();

        musicSource.clip = clip;
        musicSource.loop = false;
        musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicSource.Play();
    }

    public void PlayFootstep()
    {
        if (footstepSFX != null)
            sfxSource.PlayOneShot(footstepSFX);
    }

    public void PlayButton()
    {
        if (buttonClickSFX != null)
            sfxSource.PlayOneShot(buttonClickSFX);
    }

    public void PlayButtonHover()
    {
        if (buttonHoverSFX != null)
            sfxSource.PlayOneShot(buttonHoverSFX);
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

    public void PlayJump(string playerName)
    {
        if (playerName == "Player1" && jumpPlayer1SFX != null)
            sfxSource.PlayOneShot(jumpPlayer1SFX);
        else if (playerName == "Player2" && jumpPlayer2SFX != null)
            sfxSource.PlayOneShot(jumpPlayer2SFX);
    }

    public void PlayPolarityPositive()
    {
        if (polarityPositiveSFX != null)
            sfxSource.PlayOneShot(polarityPositiveSFX);
    }

    public void PlayPolarityNegative()
    {
        if (polarityNegativeSFX != null)
            sfxSource.PlayOneShot(polarityNegativeSFX);
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