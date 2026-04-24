using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class GearButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float rotationSpeed = 150f;      // Velocidad de rotación
    public float returnSpeed = 150f;        // Velocidad de vuelta al 0
    public PauseMenu pauseMenu;

    private RectTransform rectTransform;
    private float currentRotation = 0f;
    private bool isHovering = false;
    private Coroutine rotationCoroutine;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
        rotationCoroutine = StartCoroutine(RotateTo(-90f, rotationSpeed));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (rotationCoroutine != null) StopCoroutine(rotationCoroutine);
        rotationCoroutine = StartCoroutine(RotateTo(0f, returnSpeed));
    }

    private IEnumerator RotateTo(float targetAngle, float speed)
    {
        while (Mathf.Abs(rectTransform.localEulerAngles.z - targetAngle) > 0.1f)
        {
            float current = rectTransform.localEulerAngles.z;
            // Convertir a rango -180 a 180
            if (current > 180f) current -= 360f;
            float newAngle = Mathf.MoveTowards(current, targetAngle, speed * Time.deltaTime);
            rectTransform.localEulerAngles = new Vector3(0f, 0f, newAngle);
            yield return null;
        }
        rectTransform.localEulerAngles = new Vector3(0f, 0f, targetAngle);
    }

    public void OnClick()
    {
        pauseMenu.OpenPause();
    }
}