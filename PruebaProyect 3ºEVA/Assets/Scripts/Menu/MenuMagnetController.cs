using UnityEngine;
using UnityEngine.UI;

public class MenuMagnetController : MonoBehaviour
{
    public MagneticMenuButton[] buttons; // Los botones del menú
    public float moveSpeed = 10f;        // Velocidad del selector
    public RectTransform selector;       // Tu imán (selector)

    int currentIndex = 0;

    void Start()
    {
        // Activar el primer botón
        SelectButton(currentIndex);
    }

    void Update()
    {
        // --- Teclado ---
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangeIndex(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangeIndex(1);

        // --- Ratón ---
        CheckMouseHover();

        // Mover selector hacia el botón seleccionado
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
            Vector3 buttonPos = buttons[i].transform.position;

            // Detectar si el cursor está dentro del rectángulo del botón
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

                // Clic del ratón
                if (Input.GetMouseButtonDown(0))
                {
                    buttons[currentIndex].GetComponent<Button>().onClick.Invoke();
                }
            }
        }
    }
}