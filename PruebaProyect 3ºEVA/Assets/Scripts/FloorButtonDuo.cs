using UnityEngine;

public class FloorButtonDuo : MonoBehaviour
{
    public GameObject objectToActivate;
    public TwoPlayerZone requiredZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (requiredZone.bothPlayersInside)
            {
                objectToActivate.SetActive(true);
            }
        }
    }
}
