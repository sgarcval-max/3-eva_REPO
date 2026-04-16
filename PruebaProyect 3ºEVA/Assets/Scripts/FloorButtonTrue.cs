using UnityEngine;

public class FloorButtonTrue : MonoBehaviour
{
    public GameObject objectToDeactivate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToDeactivate.SetActive(false);
        }
    }
}