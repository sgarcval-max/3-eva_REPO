using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioManager.instance.PlayButton();
        });
    }
}