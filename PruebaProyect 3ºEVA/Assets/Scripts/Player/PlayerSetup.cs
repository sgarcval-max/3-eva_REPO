using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public bool isPositive = true;

    [Header("Sprites de polaridad")]
    public Sprite positiveSprite;  // Imagen cuando es positivo
    public Sprite negativeSprite;  // Imagen cuando es negativo

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
    }

    void UpdateSprite()
    {
        sr.sprite = isPositive ? positiveSprite : negativeSprite;
    }
}