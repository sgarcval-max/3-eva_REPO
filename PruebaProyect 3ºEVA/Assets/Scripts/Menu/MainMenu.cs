using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject mainPanel;
    public GameObject levelSelectorPanel;
    public GameObject settingsPanel;

    [Header("Imagen a ocultar")]
    public GameObject imageToHide;

    private void Start()
    {
        mainPanel.SetActive(true);
        levelSelectorPanel.SetActive(false);
        settingsPanel.SetActive(false);
        imageToHide.SetActive(true);
    }

    public void OpenLevelSelector()
    {
        mainPanel.SetActive(false);
        levelSelectorPanel.SetActive(true);
        imageToHide.SetActive(false);  // Oculta la imagen
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
        imageToHide.SetActive(true);  // Vuelve a mostrar la imagen
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}