using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles")]
    public CanvasGroup mainPanel;
    public CanvasGroup levelSelectorPanel;
    public CanvasGroup settingsPanel;

    [Header("Imagen a ocultar")]
    public GameObject imageToHide;

    private void Start()
    {
        mainPanel.alpha = 1f;
        mainPanel.interactable = true;
        mainPanel.blocksRaycasts = true;

        levelSelectorPanel.alpha = 0f;
        levelSelectorPanel.interactable = false;
        levelSelectorPanel.blocksRaycasts = false;
        levelSelectorPanel.gameObject.SetActive(false);

        settingsPanel.alpha = 0f;
        settingsPanel.interactable = false;
        settingsPanel.blocksRaycasts = false;
        settingsPanel.gameObject.SetActive(false);

        if (imageToHide != null)
            imageToHide.SetActive(true);
    }

    public void OpenLevelSelector()
    {
        if (imageToHide != null) imageToHide.SetActive(false);
        StartCoroutine(PanelFader.instance.FadeFromTo(mainPanel, levelSelectorPanel));
    }

    public void OpenSettings()
    {
        StartCoroutine(PanelFader.instance.FadeFromTo(mainPanel, settingsPanel));
    }

    public void BackToMainFromLevelSelector()
    {
        if (imageToHide != null) imageToHide.SetActive(true);
        StartCoroutine(PanelFader.instance.FadeFromTo(levelSelectorPanel, mainPanel));
    }

    public void BackToMainFromSettings()
    {
        StartCoroutine(PanelFader.instance.FadeFromTo(settingsPanel, mainPanel));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}