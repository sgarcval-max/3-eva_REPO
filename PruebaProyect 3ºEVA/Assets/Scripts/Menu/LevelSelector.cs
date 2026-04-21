using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
        // Nivel 1 siempre disponible
        level1Button.interactable = true;

        // Nivel 2 solo si nivel 1 completado
        bool level1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        level2Button.interactable = level1Completed;
        level2LockedText.gameObject.SetActive(!level1Completed);

        // Nivel 3 solo si nivel 2 completado
        bool level2Completed = PlayerPrefs.GetInt("Level2Completed", 0) == 1;
        level3Button.interactable = level2Completed;
        level3LockedText.gameObject.SetActive(!level2Completed);
    }

    public void LoadLevel1() { SceneManager.LoadScene("Level 1"); }
    public void LoadLevel2() { SceneManager.LoadScene("Level 2"); }
    public void LoadLevel3() { SceneManager.LoadScene("Level 3"); }
}
