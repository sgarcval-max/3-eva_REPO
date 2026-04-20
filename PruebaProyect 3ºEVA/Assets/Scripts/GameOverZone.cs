using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverZone : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup gameOverPanel;  // Canvas Group del panel
    public Button retryButton;
    public Button menuButton;
    public float fadeDuration = 1f;

    private void Start()
    {
        gameOverPanel.alpha = 0f;          // Oculto al inicio
        gameOverPanel.interactable = false;
        gameOverPanel.blocksRaycasts = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            StartCoroutine(ShowGameOver());
        }
    }

    private IEnumerator ShowGameOver()
    {
        gameOverPanel.interactable = true;
        gameOverPanel.blocksRaycasts = true;

        // Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            gameOverPanel.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        gameOverPanel.alpha = 1f;

        retryButton.onClick.AddListener(Retry);
        menuButton.onClick.AddListener(GoToMenu);
    }

    private void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}