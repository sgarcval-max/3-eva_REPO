using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Referencias")]
    public CountdownTimer countdownTimer;

    private bool isPaused = false;

    private void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OpenPause()
    {
        Debug.Log("OpenPause llamado, timeScale = " + Time.timeScale);
        isPaused = true;
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
        Time.timeScale = 0f;
        countdownTimer.StopTimer();
        Debug.Log("timeScale después = " + Time.timeScale);
    }

    public void Resume()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        countdownTimer.ResumeTimer();
    }

    public void OpenSettings()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToPause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}