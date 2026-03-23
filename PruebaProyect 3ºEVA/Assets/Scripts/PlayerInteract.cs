using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    private List<EnergyGenerator> generators = new List<EnergyGenerator>();
    private List<BrokenGenerator> brokenGenerators = new List<BrokenGenerator>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnergyGenerator gen = other.GetComponent<EnergyGenerator>();
        if (gen != null && !generators.Contains(gen))
            generators.Add(gen);

        BrokenGenerator brokenGen = other.GetComponent<BrokenGenerator>();
        if (brokenGen != null && !brokenGenerators.Contains(brokenGen))
            brokenGenerators.Add(brokenGen);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        EnergyGenerator gen = other.GetComponent<EnergyGenerator>();
        if (gen != null && generators.Contains(gen))
            generators.Remove(gen);

        BrokenGenerator brokenGen = other.GetComponent<BrokenGenerator>();
        if (brokenGen != null && brokenGenerators.Contains(brokenGen))
            brokenGenerators.Remove(brokenGen);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // Interactuar con todos los generadores normales cercanos
        foreach (var gen in generators)
            gen.Interact();

        // Interactuar con todos los generadores rotos cercanos
        foreach (var brokenGen in brokenGenerators)
            brokenGen.Interact();
    }
}
