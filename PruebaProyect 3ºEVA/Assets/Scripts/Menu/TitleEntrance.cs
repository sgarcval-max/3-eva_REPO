using UnityEngine;
using TMPro;
using System.Collections;

public class TitleEntrance : MonoBehaviour
{
    public RectTransform titleImage;   // Imagen del título
    public float duration = 1f;        // Duración de la animación
    private CanvasGroup canvasGroup;
    private Vector3 originalScale;

    void Start()
    {
        // Guardar escala original
        originalScale = titleImage.localScale;

        // Empezar desde 0
        titleImage.localScale = Vector3.zero;

        // Configurar CanvasGroup para el fade
        canvasGroup = titleImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = titleImage.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;

        // Iniciar animación de entrada
        StartCoroutine(AnimateEntrance());
    }

    IEnumerator AnimateEntrance()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Escalar de 0 a original
            titleImage.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);

            // Fade in
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        // Asegurar valores finales
        titleImage.localScale = originalScale;
        canvasGroup.alpha = 1f;
    }
}
