using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    public Transform downPoint;
    public Transform upPoint;

    public float speed = 2f;

    private Vector3 target;
    private bool moving = false;

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                moving = false;
            }
        }
    }

    public void MoveDown()
    {
        target = downPoint.position;
        moving = true;
    }

    public void MoveUp()
    {
        target = upPoint.position;
        moving = true;
    }
}
