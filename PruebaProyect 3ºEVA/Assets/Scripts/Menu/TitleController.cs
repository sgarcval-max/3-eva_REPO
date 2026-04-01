using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleController : MonoBehaviour
{
    [Header("UI Elements")]
    public RectTransform titleImage;       // Imagen del título
    public GameObject pressSpaceText;      // Texto "Presiona ESPACIO"
    public RectTransform buttonsContainer; // Contenedor de botones

    [Header("Animación")]
    public float animationDuration = 1f;       // Duración del movimiento + escalado
    public float scaleFactor = 0.7f;           // Escala final del título
    public Vector2 moveDirection = new Vector2(0, 200); // Dirección y distancia del movimiento

    private Vector2 titleStartPos;
    private Vector3 titleStartScale;
    private bool started = false;

    void Start()
    {
        // Guardar posición y escala inicial
        titleStartPos = titleImage.anchoredPosition;
        titleStartScale = titleImage.localScale;

        // Ocultar botones al inicio
        buttonsContainer.gameObject.SetActive(false);

        // Desactivar animaciones de botones al inicio
        foreach (var b in buttonsContainer.GetComponentsInChildren<Button>())
        {
            ButtonScaleEntrance entrance = b.GetComponent<ButtonScaleEntrance>();
            if (entrance != null)
                entrance.enabled = false;
        }
    }

    void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
            StartCoroutine(AnimateTitle());
        }
    }

    IEnumerator AnimateTitle()
    {
        // 1️⃣ Desaparecer el texto
        pressSpaceText.SetActive(false);

        // 2️⃣ Animación simultánea de escala y movimiento
        float elapsed = 0f;
        Vector2 targetPos = titleStartPos + moveDirection;
        Vector3 targetScale = titleStartScale * scaleFactor;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animationDuration;

            // Escalar
            titleImage.localScale = Vector3.Lerp(titleStartScale, targetScale, t);

            // Mover en la dirección deseada
            titleImage.anchoredPosition = Vector2.Lerp(titleStartPos, targetPos, t);

            yield return null;
        }

        // Valores finales exactos
        titleImage.localScale = targetScale;
        titleImage.anchoredPosition = targetPos;

        // 3️⃣ Activar botones y animaciones
        buttonsContainer.gameObject.SetActive(true);
        foreach (var b in buttonsContainer.GetComponentsInChildren<Button>())
        {
            ButtonScaleEntrance entrance = b.GetComponent<ButtonScaleEntrance>();
            if (entrance != null)
                entrance.enabled = true;
        }
    }
}