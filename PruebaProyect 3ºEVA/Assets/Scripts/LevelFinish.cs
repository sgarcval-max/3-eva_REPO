using UnityEngine;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    public TextMeshProUGUI winText;

    private int playersInside = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside++;

            if (playersInside >= 2)
            {
                winText.gameObject.SetActive(true);
                winText.text = "NIVEL COMPLETADO";
            }
        }
    }
}