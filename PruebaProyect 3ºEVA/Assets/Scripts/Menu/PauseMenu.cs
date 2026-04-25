using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Paneles")]
    public CanvasGroup pausePanel;
    public CanvasGroup settingsPanel;

    [Header("Referencias")]
    public CountdownTimer countdownTimer;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.alpha = 0f;
        pausePanel.interactable = false;
        pausePanel.blocksRaycasts = false;
        pausePanel.gameObject.SetActive(false);

        settingsPanel.alpha = 0f;
        settingsPanel.interactable = false;
        settingsPanel.blocksRaycasts = false;
        settingsPanel.gameObject.SetActive(false);
    }

    public void OpenPause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        countdownTimer.StopTimer();
        StartCoroutine(PanelFader.instance.FadeIn(pausePanel));
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        countdownTimer.ResumeTimer();
        StartCoroutine(PanelFader.instance.FadeOut(pausePanel));
    }

    public void OpenSettings()
    {
        StartCoroutine(PanelFader.instance.FadeFromTo(pausePanel, settingsPanel));
    }

    public void BackToPause()
    {
        StartCoroutine(PanelFader.instance.FadeFromTo(settingsPanel, pausePanel));
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        FadeManager.instance.LoadScene("MainMenu");
    }
}