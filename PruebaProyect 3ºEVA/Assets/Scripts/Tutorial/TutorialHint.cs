using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialHint : MonoBehaviour
{
    [Header("Configuración")]
    [TextArea] public string message;
    public float offsetY = 1.5f;        // Altura sobre el objeto
    public float fadeDuration = 0.3f;

    [Header("Referencias")]
    public GameObject hintPanel;        // Panel con el texto y la flecha
    public TextMeshProUGUI hintText;    // Texto del mensaje
    public CanvasGroup hintCanvasGroup; // Canvas Group del panel

    private bool isShowing = false;
    private bool hasBeenShown = false;  // Para que solo se muestre una vez

    private void Start()
    {
        hintCanvasGroup.alpha = 0f;
        hintCanvasGroup.interactable = false;
        hintCanvasGroup.blocksRaycasts = false;
        hintPanel.SetActive(false);
    }

    private void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(HideHint());
        }

        // Mantener el panel encima del objeto
        if (isShowing && hintPanel != null)
        {
            Vector3 worldPos = transform.position + new Vector3(0f, offsetY, 0f);
            hintPanel.transform.position = Camera.main.WorldToScreenPoint(worldPos);
        }
    }

    public void ShowHint()
    {
        if (hasBeenShown) return;
        hasBeenShown = true;
        StartCoroutine(ShowHintCoroutine());
    }

    private IEnumerator ShowHintCoroutine()
    {
        isShowing = true;
        hintText.text = message + "\n<size=70%><color=#aaaaaa>Pulsa INTRO para continuar</color></size>";
        hintPanel.SetActive(true);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            hintCanvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        hintCanvasGroup.alpha = 1f;
    }

    private IEnumerator HideHint()
    {
        isShowing = false;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            hintCanvasGroup.alpha = Mathf.Clamp01(1f - elapsed / fadeDuration);
            yield return null;
        }
        hintCanvasGroup.alpha = 0f;
        hintPanel.SetActive(false);
    }
}