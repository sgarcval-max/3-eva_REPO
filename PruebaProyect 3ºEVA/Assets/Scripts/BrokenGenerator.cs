using UnityEngine;

public class BrokenGenerator : MonoBehaviour
{
    public LiftPlatform platform;

    private bool playerInside = false;
    private int state = 0;

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

        Debug.Log("Interactuó con BrokenGenerator");

        if (state == 0)
        {
            platform.MoveDown();
            state = 1;
        }
        else if (state == 1)
        {
            platform.MoveUp();
            state = 2;
        }
    }
}