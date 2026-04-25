using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Button = UnityEngine.UI.Button;

public class CountdownTimer : MonoBehaviour
{
    [Header("Tiempo")]
    public float totalTime = 60f;
    private float currentTime;
    private bool timerRunning = true;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public CanvasGroup gameOverPanel;
    public Button retryButton;
    public Button menuButton;
    public float fadeDuration = 1f;

    private void Start()
    {
        currentTime = totalTime;
        gameOverPanel.alpha = 0f;
        gameOverPanel.interactable = false;
        gameOverPanel.blocksRaycasts = false;
    }

    private void Update()
    {
        if (!timerRunning) return;
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;
            timerText.text = FormatTime(0f);
            StartCoroutine(ShowGameOver());
        }
        else
        {
            timerText.text = FormatTime(currentTime);
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.CeilToInt(time % 60f);

        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
        }

        if (minutes >= 1)
            return string.Format("{0:00}:{1:00} mins", minutes, seconds);
        else
            return string.Format("{0:00} s", seconds);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        timerRunning = true;
    }

    private IEnumerator ShowGameOver()
    {
        // Esperamos a que termine el fade antes de pausar
        gameOverPanel.interactable = true;
        gameOverPanel.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            gameOverPanel.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        gameOverPanel.alpha = 1f;
        Time.timeScale = 0f;  // Pausar el juego cuando el panel ya está visible

        retryButton.onClick.AddListener(Retry);
        menuButton.onClick.AddListener(GoToMenu);
    }

    private void Retry()
    {
        Time.timeScale = 1f;  // Reanudar antes de reiniciar
        FadeManager.instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;  // Reanudar antes de ir al menú
        FadeManager.instance.LoadScene("MainMenu");
    }
}