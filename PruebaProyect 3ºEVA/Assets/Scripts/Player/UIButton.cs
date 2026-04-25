using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private void Start()
    {
        // Por si acaso también detecta el click normal
        Button btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(() => AudioManager.instance.PlayButton());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.PlayButtonHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlayButton();
    }
}
