using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using Button = UnityEngine.UI.Button;
using CanvasGroup = UnityEngine.CanvasGroup;

public class LevelFinish2 : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup winPanel;
    public TextMeshProUGUI winText;
    public Button nextLevelButton;
    public Button menuButton;
    public float fadeDuration = 1f;

    [Header("Referencias")]
    public CountdownTimer countdownTimer;
    public string nextLevelName;

    private int playersInside = 0;
    private bool finished = false;

    private void Start()
    {
        winPanel.alpha = 0f;
        winPanel.interactable = false;
        winPanel.blocksRaycasts = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !finished)
        {
            playersInside++;
            if (playersInside >= 2)
            {
                finished = true;
                PlayerPrefs.SetInt("Level2Completed", 1);
                PlayerPrefs.Save();
                countdownTimer.StopTimer();
                StartCoroutine(ShowWinPanel());
            }
        }
    }

    private IEnumerator ShowWinPanel()
    {
        winText.text = "NIVEL COMPLETADO";
        winPanel.interactable = true;
        winPanel.blocksRaycasts = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            winPanel.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        winPanel.alpha = 1f;

        nextLevelButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(GoToMenu);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}