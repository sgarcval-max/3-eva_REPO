using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LevelFinishLast : MonoBehaviour
{
    [Header("UI")]
    public CanvasGroup winPanel;
    public TextMeshProUGUI winText;
    public Button menuButton;
    public float fadeDuration = 1f;

    [Header("Referencias")]
    public CountdownTimer countdownTimer;

    private int playersInside = 0;
    private bool finished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !finished)
        {
            playersInside++;
            if (playersInside >= 2)
            {
                finished = true;
                countdownTimer.StopTimer();
                StartCoroutine(ShowWinPanel());
            }
        }
    }

    private IEnumerator ShowWinPanel()
    {
        winText.text = "JUEGO COMPLETADO";
        winPanel.interactable = true;
        winPanel.blocksRaycasts = true;

        PlayerPrefs.SetInt("Level2Completed", 1);
        PlayerPrefs.Save();

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            winPanel.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        winPanel.alpha = 1f;

        menuButton.onClick.AddListener(GoToMenu);
    }

    private void GoToMenu()
    {
        FadeManager.instance.LoadScene("MainMenu");
    }
}