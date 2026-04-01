using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuMagnetController : MonoBehaviour
{
    [Header("Configuración del menú")]
    public MagneticMenuButton[] buttons;
    public RectTransform selector;
    public float moveSpeed = 10f;
    public float fadeDuration = 0.5f;

    [HideInInspector]
    public bool canSelect = false;

    private int currentIndex = 0;
    private CanvasGroup selectorCanvas;

    void Start()
    {
        if (selector != null)
        {
            selectorCanvas = selector.GetComponent<CanvasGroup>();
            if (selectorCanvas == null)
                selectorCanvas = selector.gameObject.AddComponent<CanvasGroup>();

            selectorCanvas.alpha = 0f;
            selectorCanvas.interactable = false;
            selectorCanvas.blocksRaycasts = false;
        }

        // Aseguramos que solo el primer botón esté seleccionado
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].isSelected = (i == currentIndex);
    }

    void Update()
    {
        // Esperamos a que los botones terminen su animación
        if (!canSelect)
        {
            bool allFinished = true;
            foreach (var b in buttons)
            {
                ButtonScaleEntrance entrance = b.GetComponent<ButtonScaleEntrance>();
                if (entrance != null && !entrance.isFinished)
                {
                    allFinished = false;
                    break;
                }
            }

            if (allFinished)
            {
                canSelect = true;
                StartCoroutine(FadeInSelector());
            }
        }

        if (!canSelect || selectorCanvas.alpha < 1f) return;

        // --- INPUT TECLADO ---
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangeIndex(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangeIndex(1);

        // Mover selector suavemente
        Vector3 targetPos = new Vector3(selector.position.x, buttons[currentIndex].transform.position.y, selector.position.z);
        selector.position = Vector3.Lerp(selector.position, targetPos, Time.deltaTime * moveSpeed);

        // Ejecutar acción
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Sincronizamos currentIndex con el botón que está realmente seleccionado
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].isSelected)
                {
                    currentIndex = i;
                    break;
                }
            }

            buttons[currentIndex].GetComponent<Button>().onClick.Invoke();
        }
    }

    IEnumerator FadeInSelector()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            selectorCanvas.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        selectorCanvas.alpha = 1f;
        selectorCanvas.interactable = true;
        selectorCanvas.blocksRaycasts = true;
    }

    void ChangeIndex(int dir)
    {
        buttons[currentIndex].isSelected = false;
        currentIndex += dir;
        if (currentIndex < 0) currentIndex = buttons.Length - 1;
        if (currentIndex >= buttons.Length) currentIndex = 0;
        buttons[currentIndex].isSelected = true;
    }
}