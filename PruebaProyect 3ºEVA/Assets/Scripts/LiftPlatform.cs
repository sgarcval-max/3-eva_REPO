using UnityEngine;

public class LiftPlatform : MonoBehaviour
{
    public Transform downPoint;
    public Transform upPoint;
    public float speed = 2f;
    private Vector3 target;
    private bool moving = false;
    private bool isUp = false;
    private bool playerOnPlatform = false;
    private bool missionComplete = false;

    void Start()
    {
        target = transform.position;
    }

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
                if (isUp && playerOnPlatform)
                    missionComplete = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            playerOnPlatform = false;
    }

    public bool IsMissionComplete()
    {
        return missionComplete;
    }

    // Resetea la mision para que el generador vuelva a funcionar
    public void ResetMission()
    {
        missionComplete = false;
    }

    public void MoveDown()
    {
        target = downPoint.position;
        moving = true;
        isUp = false;
    }

    public void MoveUp()
    {
        target = upPoint.position;
        moving = true;
        isUp = true;
    }

    public void Toggle()
    {
        if (isUp)
            MoveDown();
        else
            MoveUp();
    }
}