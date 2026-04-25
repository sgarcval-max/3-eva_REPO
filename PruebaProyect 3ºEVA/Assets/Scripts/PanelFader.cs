using UnityEngine;
using System.Collections;

public class PanelFader : MonoBehaviour
{
    public static PanelFader instance;

    public float fadeDuration = 0.3f;

    private void Awake()
    {
        instance = this;
    }

    public IEnumerator FadeOut(CanvasGroup panel)
    {
        panel.interactable = false;
        panel.blocksRaycasts = false;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            panel.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }
        panel.alpha = 0f;
        panel.gameObject.SetActive(false);
    }

    public IEnumerator FadeIn(CanvasGroup panel)
    {
        panel.gameObject.SetActive(true);
        panel.alpha = 0f;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            panel.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }
        panel.alpha = 1f;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    public IEnumerator FadeFromTo(CanvasGroup from, CanvasGroup to)
    {
        yield return StartCoroutine(FadeOut(from));
        yield return StartCoroutine(FadeIn(to));
    }
}