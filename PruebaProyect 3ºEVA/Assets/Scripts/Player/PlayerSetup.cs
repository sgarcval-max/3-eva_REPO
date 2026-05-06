using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public bool isPositive = true;

    [Header("Sprites de polaridad")]
    public Sprite positiveSprite;
    public Sprite negativeSprite;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }

    public void TogglePolarity(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        isPositive = !isPositive;
        UpdateSprite();

        if (isPositive)
            AudioManager.instance.PlayPolarityPositive();
        else
            AudioManager.instance.PlayPolarityNegative();
    }

    void UpdateSprite()
    {
        sr.sprite = isPositive ? positiveSprite : negativeSprite;
    }
}