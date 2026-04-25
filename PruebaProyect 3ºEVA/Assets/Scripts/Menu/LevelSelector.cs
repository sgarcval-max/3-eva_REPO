using UnityEngine;
using TMPro;
using Button = UnityEngine.UI.Button;

public class LevelSelector : MonoBehaviour
{
    [Header("Botones de niveles")]
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    [Header("Textos de bloqueado")]
    public TextMeshProUGUI level2LockedText;
    public TextMeshProUGUI level3LockedText;

    private void Start()
    {
        UpdateLevelButtons();
    }

    private void UpdateLevelButtons()
    {
        level1Button.interactable = true;

        bool level1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        level2Button.interactable = level1Completed;
        if (level2LockedText != null)
            level2LockedText.gameObject.SetActive(!level1Completed);

        bool level2Completed = PlayerPrefs.GetInt("Level2Completed", 0) == 1;
        level3Button.interactable = level2Completed;
        if (level3LockedText != null)
            level3LockedText.gameObject.SetActive(!level2Completed);
    }

    public void LoadLevel1() { FadeManager.instance.LoadScene("Level 1"); }
    public void LoadLevel2() { FadeManager.instance.LoadScene("Level 2"); }
    public void LoadLevel3() { FadeManager.instance.LoadScene("Level 3"); }

    [ContextMenu("Resetear Progreso")]
    private void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progreso reseteado!");
    }
}