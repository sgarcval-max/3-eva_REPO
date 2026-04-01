using UnityEngine;
using TMPro;
using System.Collections;

public class BlinkTMPText : MonoBehaviour
{
    public TMP_Text targetText;        // Texto TMP que queremos hacer parpadear
    public float fadeDuration = 0.5f;  // Duración de cada fade (entrada/salida)
    public float minAlpha = 0.2f;      // Opacidad mínima durante el parpadeo

    private CanvasGroup canvasGroup;

    void Start()
    {
        // Crear CanvasGroup si no existe
        canvasGroup = targetText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = targetText.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 1f; // Empezar completamente visible

        // Iniciar parpadeo
        StartCoroutine(BlinkLoop());
    }

    IEnumerator BlinkLoop()
    {
        while (true)
        {
            // Fade out
            float elapsed = 0f;
            float startAlpha = canvasGroup.alpha;
            float endAlpha = minAlpha;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
                yield return null;
            }

            // Fade in
            elapsed = 0f;
            startAlpha = canvasGroup.alpha;
            endAlpha = 1f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
                yield return null;
            }
        }
    }
}
