using UnityEngine;

public class TwoPlayerZone : MonoBehaviour
{
    public bool bothPlayersInside = false;

    private int playersInside = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside++;

            if (playersInside >= 2)
            {
                bothPlayersInside = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside--;

            if (playersInside < 2)
            {
                bothPlayersInside = false;
            }
        }
    }
}