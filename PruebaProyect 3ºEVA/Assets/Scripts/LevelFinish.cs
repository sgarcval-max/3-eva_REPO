using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LevelFinish : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup winPanel;
    public TextMeshProUGUI winText;
    public Button nextLevelButton;
    public Button menuButton;
    public float fadeDuration = 1f;

    [Header("Referencias")]
    public CountdownTimer countdownTimer;

    private bool player1Inside = false;
    private bool player2Inside = false;
    private bool finished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !finished)
        {
            if (other.gameObject.name == "Player1")
                player1Inside = true;
            else if (other.gameObject.name == "Player2")
                player2Inside = true;

            if (player1Inside && player2Inside)
            {
                finished = true;
                countdownTimer.StopTimer();
                StartCoroutine(ShowWinPanel());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !finished)
        {
            if (other.gameObject.name == "Player1")
                player1Inside = false;
            else if (other.gameObject.name == "Player2")
                player2Inside = false;
        }
    }

    private IEnumerator ShowWinPanel()
    {
        winText.text = "NIVEL COMPLETADO";
        winPanel.interactable = true;
        winPanel.blocksRaycasts = true;
        PlayerPrefs.SetInt("Level1Completed", 1);
        PlayerPrefs.Save();

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
        FadeManager.instance.LoadScene("Level 2");
    }

    private void GoToMenu()
    {
        FadeManager.instance.LoadScene("MainMenu");
    }
}