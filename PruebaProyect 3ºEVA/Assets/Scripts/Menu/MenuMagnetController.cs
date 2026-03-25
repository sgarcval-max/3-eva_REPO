using UnityEngine;
using UnityEngine.UI;

public class MenuMagnetController : MonoBehaviour
{
    [Header("Configuración del menú")]
    public MagneticMenuButton[] buttons;
    public RectTransform selector;
    public float moveSpeed = 10f;

    [HideInInspector]
    public bool canSelect = false; // Controla cuando el selector puede interactuar

    private int currentIndex = 0;
    private CanvasGroup selectorCanvas;

    void Start()
    {
        // Configuramos el CanvasGroup del selector
        if (selector != null)
        {
            selectorCanvas = selector.GetComponent<CanvasGroup>();
            if (selectorCanvas == null)
                selectorCanvas = selector.gameObject.AddComponent<CanvasGroup>();

            // Invisible e inactivo al inicio
            selectorCanvas.alpha = 0f;
            selectorCanvas.interactable = false;
            selectorCanvas.blocksRaycasts = false;
        }

        // Seleccionamos el primer botón (solo visual, no afecta a la atracción)
        SelectButton(currentIndex);
    }

    void Update()
    {
        // --- Verificamos si todos los botones terminaron la animación de entrada ---
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

                // Hacemos visible e interactivo el selector
                if (selectorCanvas != null)
                {
                    selectorCanvas.alpha = 1f;
                    selectorCanvas.interactable = true;
                    selectorCanvas.blocksRaycasts = true;
                }
            }
        }

        // --- Si la animación no ha terminado, no hacemos nada más ---
        if (!canSelect) return;

        // --- Input teclado ---
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangeIndex(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangeIndex(1);

        // --- Input ratón ---
        CheckMouseHover();

        // Mover selector hacia el botón seleccionado (solo visual)
        Vector3 targetPos = new Vector3(selector.position.x, buttons[currentIndex].transform.position.y, selector.position.z);
        selector.position = Vector3.Lerp(selector.position, targetPos, Time.deltaTime * moveSpeed);

        // Seleccionar con Enter
        if (Input.GetKeyDown(KeyCode.Return))
            buttons[currentIndex].GetComponent<Button>().onClick.Invoke();
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