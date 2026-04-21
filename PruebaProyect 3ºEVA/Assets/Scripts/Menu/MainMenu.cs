using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject mainPanel;
    public GameObject levelSelectorPanel;
    public GameObject settingsPanel;

    private void Start()
    {
        mainPanel.SetActive(true);
        levelSelectorPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OpenLevelSelector()
    {
        mainPanel.SetActive(false);
        levelSelectorPanel.SetActive(true);
    }

    public void OpenSettings()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMain()
    {
        mainPanel.SetActive(true);
        levelSelectorPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
