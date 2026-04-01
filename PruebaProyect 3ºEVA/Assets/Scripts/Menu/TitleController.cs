using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TitleController : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform titleImage;       // Imagen del título
    public TMP_Text pressSpaceText;        // Texto TMP "Presiona ESPACIO"
    public RectTransform buttonsContainer; // Contenedor de botones

    [Header("Animations")]
    public float entranceDuration = 1f;    // Animación de entrada (escala + fade)
    public float spaceAnimDuration = 1f;   // Animación al pulsar ESPACIO
    public float scaleFactor = 0.7f;       // Escala final al mover hacia arriba
    public Vector2 moveDirection = new Vector2(0, 200); // Dirección de movimiento al pulsar ESPACIO
    public float blinkFadeDuration = 0.5f; // Duración fade in/out del texto parpadeante
    public float blinkMinAlpha = 0.2f;     // Opacidad mínima del parpadeo

    private CanvasGroup titleCanvasGroup;
    private Vector3 originalScale;
    private Vector2 originalPos;
    private bool entranceFinished = false;
    private bool spacePressed = false;

    void Start()
    {
        // Guardar escala y posición original
        originalScale = titleImage.localScale;
        originalPos = titleImage.anchoredPosition;

        // Iniciar desde 0 y fade 0
        titleImage.localScale = Vector3.zero;
        titleCanvasGroup = titleImage.GetComponent<CanvasGroup>();
        if (titleCanvasGroup == null)
            titleCanvasGroup = titleImage.gameObject.AddComponent<CanvasGroup>();
        titleCanvasGroup.alpha = 0f;

        // Asegurarse de que el texto TMP esté activo
        if (pressSpaceText != null)
            pressSpaceText.gameObject.SetActive(true);

        // Ocultar botones
        buttonsContainer.gameObject.SetActive(false);
        foreach (var b in buttonsContainer.GetComponentsInChildren<Button>())
        {
            var entrance = b.GetComponent<ButtonScaleEntrance>();
            if (entrance != null) entrance.enabled = false;
        }

        // Iniciar animación de entrada
        StartCoroutine(EntranceAnimation());

        // Iniciar parpadeo del texto
        if (pressSpaceText != null)
            StartCoroutine(BlinkText());
    }

    void Update()
    {
        if (!spacePressed && entranceFinished && Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
            pressSpaceText.gameObject.SetActive(false);
            StartCoroutine(SpaceAnimation());
        }
    }

    IEnumerator EntranceAnimation()
    {
        float elapsed = 0f;
        while (elapsed < entranceDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / entranceDuration;
            titleImage.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            titleCanvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        titleImage.localScale = originalScale;
        titleCanvasGroup.alpha = 1f;
        entranceFinished = true;
    }

    IEnumerator SpaceAnimation()
    {
        Vector2 startPos = titleImage.anchoredPosition;
        Vector3 startScale = titleImage.localScale;
        Vector2 targetPos = originalPos + moveDirection;
        Vector3 targetScale = originalScale * scaleFactor;

        float elapsed = 0f;
        while (elapsed < spaceAnimDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / spaceAnimDuration;
            titleImage.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            titleImage.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
        titleImage.anchoredPosition = targetPos;
        titleImage.localScale = targetScale;

        // Activar botones y animaciones
        buttonsContainer.gameObject.SetActive(true);
        foreach (var b in buttonsContainer.GetComponentsInChildren<Button>())
        {
            var entrance = b.GetComponent<ButtonScaleEntrance>();
            if (entrance != null) entrance.enabled = true;
        }
    }

    IEnumerator BlinkText()
    {
        CanvasGroup textCanvas = pressSpaceText.GetComponent<CanvasGroup>();
        if (textCanvas == null)
            textCanvas = pressSpaceText.gameObject.AddComponent<CanvasGroup>();
        textCanvas.alpha = 1f;

        while (!spacePressed)
        {
            // Fade out
            float elapsed = 0f;
            while (elapsed < blinkFadeDuration)
            {
                elapsed += Time.deltaTime;
                textCanvas.alpha = Mathf.Lerp(1f, blinkMinAlpha, elapsed / blinkFadeDuration);
                yield return null;
            }

            // Fade in
            elapsed = 0f;
            while (elapsed < blinkFadeDuration)
            {
                elapsed += Time.deltaTime;
                textCanvas.alpha = Mathf.Lerp(blinkMinAlpha, 1f, elapsed / blinkFadeDuration);
                yield return null;
            }
        }

        textCanvas.alpha = 0f; // Asegurar que desaparece al presionar ESPACIO
    }
}