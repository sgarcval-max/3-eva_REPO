using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    public LiftPlatform platform;
    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
    }

    public void Interact()
    {
        if (!playerInside) return;

        // Si el player ya llego arriba en la plataforma el generador no funciona
        if (platform.IsMissionComplete())
        {
            Debug.Log("Mision completada, el generador ya no funciona");
            return;
        }

        platform.Toggle();
    }
}
