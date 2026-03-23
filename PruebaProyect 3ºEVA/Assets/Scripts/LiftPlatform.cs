using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    public float height = 5f;
    public float speed = 2f;

    private bool activated = false;
    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * height;
    }

    void Update()
    {
        if (activated)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    public void ActivateLift()
    {
        if (!activated)
        {
            activated = true;
        }
    }
}
