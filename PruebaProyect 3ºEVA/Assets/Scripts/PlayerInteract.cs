using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    private EnergyGenerator generatorInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnergyGenerator gen = other.GetComponent<EnergyGenerator>();

        if (gen != null)
        {
            generatorInRange = gen;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnergyGenerator gen = other.GetComponent<EnergyGenerator>();

        if (gen != null)
        {
            generatorInRange = null;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && generatorInRange != null)
        {
            generatorInRange.Interact();
        }
    }
}
