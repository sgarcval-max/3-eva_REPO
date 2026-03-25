using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuMagnetController : MonoBehaviour
{
    [Header("Configuración del menú")]
    public MagneticMenuButton[] buttons;
    public RectTransform selector;
    public float moveSpeed = 10f;
    public float fadeDuration = 0.5f;   // Duración del fade-in

    [HideInInspector]
    public bool canSelect = false;

    private int currentIndex = 0;
    private CanvasGroup selectorCanvas;

    void Start()
    {
        // Configuramos CanvasGroup
        if (selector != null)
        {
            selectorCanvas = selector.GetComponent<CanvasGroup>();
            if (selectorCanvas == null)
                selectorCanvas = selector.gameObject.AddComponent<CanvasGroup>();

            selectorCanvas.alpha = 0f;
            selectorCanvas.interactable = false;
            selectorCanvas.blocksRaycasts = false;
        }

        SelectButton(currentIndex);
    }

    void Update()
    {
        // Verificamos si todos los botones terminaron la animación
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

        // Si el selector todavía no está activo, no hacemos input
        if (!canSelect || selectorCanvas.alpha < 1f) return;

        // --- Input teclado ---
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangeIndex(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangeIndex(1);

        // --- Input ratón ---
        CheckMouseHover();

        // Mover selector hacia el botón seleccionado
        Vector3 targetPos = new Vector3(selector.position.x, buttons[currentIndex].transform.position.y, selector.position.z);
        selector.position = Vector3.Lerp(selector.position, targetPos, Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.Return))
            buttons[currentIndex].GetComponent<Button>().onClick.Invoke();
    }

    IEnumerator FadeInSelector()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            selectorCanvas.alpha = alpha;
            yield return null;
        }

        // Al final del fade, hacemos interactivo el selector
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
        SelectButton(currentIndex);
    }

    void SelectButton(int index)
    {
        buttons[index].isSelected = true;
    }

    void CheckMouseHover()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Vector3 mousePos = Input.mousePosition;
            RectTransform rt = buttons[i].GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            if (mousePos.x >= corners[0].x && mousePos.x <= corners[2].x &&
                mousePos.y >= corners[0].y && mousePos.y <= corners[2].y)
            {
                if (currentIndex != i)
                {
                    buttons[currentIndex].isSelected = false;
                    currentIndex = i;
                    buttons[currentIndex].isSelected = true;
                }

                if (Input.GetMouseButtonDown(0))
                    buttons[currentIndex].GetComponent<Button>().onClick.Invoke();
            }
        }
    }
}