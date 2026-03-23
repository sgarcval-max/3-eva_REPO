using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSetup : MonoBehaviour
{
    public bool isPositive = true;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    public void TogglePolarity(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        isPositive = !isPositive;
        UpdateColor();
    }

    void UpdateColor()
    {
        sr.color = isPositive ? Color.red : Color.blue;
    }
}