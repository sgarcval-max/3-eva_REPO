using UnityEngine;
using System.Collections;

public class FloorButtonTwoPlayers : MonoBehaviour
{
    public GameObject platform;
    public Transform targetPosition;
    public float moveSpeed = 3f;

    private int playersOnButton = 0;  // Contador de players en el botón
    private bool activated = false;   // Para que no se active dos veces

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnButton++;  // Suma 1 cuando entra un player

            if (playersOnButton >= 2 && !activated)
            {
                activated = true;
                StartCoroutine(MovePlatform());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersOnButton--;  // Resta 1 cuando sale un player
        }
    }

    private IEnumerator MovePlatform()
    {
        while (Vector2.Distance(platform.transform.position, targetPosition.position) > 0.05f)
        {
            platform.transform.position = Vector2.MoveTowards(
                platform.transform.position,
                targetPosition.position,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}
