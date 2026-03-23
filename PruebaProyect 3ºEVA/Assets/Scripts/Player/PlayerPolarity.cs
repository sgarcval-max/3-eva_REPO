using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPolarity : MonoBehaviour
{
    public bool isPositive = true; // true = polo +, false = polo -
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    public void ChangePolarity(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isPositive = !isPositive;
            UpdateColor();
        }
    }

    void UpdateColor()
    {
        sr.color = isPositive ? Color.red : Color.blue;
    }
}