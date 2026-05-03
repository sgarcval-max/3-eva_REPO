using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    [Header("Fade")]
    public float fadeDuration = 1f;

    private CanvasGroup fadePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            CreateFadePanel();
        }
        else
        {
            Destroy(gameObject);
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
        StartCoroutine(FadeIn());
    }

    private void CreateFadePanel()
    {
        GameObject canvasObj = new GameObject("FadeCanvas");
        canvasObj.transform.SetParent(transform);
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999;

        GameObject panelObj = new GameObject("FadePanel");
        panelObj.transform.SetParent(canvasObj.transform);
        UnityEngine.UI.Image image = panelObj.AddComponent<UnityEngine.UI.Image>();
        image.color = Color.black;

        RectTransform rect = panelObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        fadePanel = panelObj.AddComponent<CanvasGroup>();
        fadePanel.alpha = 1f;
        fadePanel.blocksRaycasts = true;
    }

    public IEnumerator FadeIn()
    {
        fadePanel.alpha = 1f;
        fadePanel.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadePanel.alpha = Mathf.Lerp(1f, 0f, t);

            // Fade in de audio
            if (AudioManager.instance != null)
                AudioManager.instance.musicSource.volume = Mathf.Lerp(0f, PlayerPrefs.GetFloat("MusicVolume", 1f), t);

            yield return null;
        }

        fadePanel.alpha = 0f;
        fadePanel.blocksRaycasts = false;

        if (AudioManager.instance != null)
            AudioManager.instance.musicSource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    public IEnumerator FadeToScene(string sceneName)
    {
        fadePanel.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadePanel.alpha = Mathf.Lerp(0f, 1f, t);

            // Fade out de audio
            if (AudioManager.instance != null)
                AudioManager.instance.musicSource.volume = Mathf.Lerp(PlayerPrefs.GetFloat("MusicVolume", 1f), 0f, t);

            yield return null;
        }

        fadePanel.alpha = 1f;

        if (AudioManager.instance != null)
            AudioManager.instance.musicSource.volume = 0f;

        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeToScene(sceneName));
    }
}
