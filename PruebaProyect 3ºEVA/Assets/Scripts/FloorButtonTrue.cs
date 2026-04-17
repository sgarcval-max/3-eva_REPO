using UnityEngine;
using System.Collections;

public class FloorButtonTrue : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform targetPosition;
    public float moveSpeed = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveAndDisappear());
        }
    }

    private IEnumerator MoveAndDisappear()
    {
        while (Vector2.Distance(objectToMove.transform.position, targetPosition.position) > 0.05f)
        {
            objectToMove.transform.position = Vector2.MoveTowards(
                objectToMove.transform.position,
                targetPosition.position,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        objectToMove.SetActive(false);
    }
}