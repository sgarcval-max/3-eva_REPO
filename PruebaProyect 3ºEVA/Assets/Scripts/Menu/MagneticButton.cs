using UnityEngine;

public class MagneticButton : MonoBehaviour
{
    public float magnetStrength = 50f;
    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        float distance = Vector3.Distance(transform.position, mousePos);

        if (distance < 200)
        {
            Vector3 dir = (mousePos - transform.position).normalized;
            transform.position += dir * magnetStrength * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * 5);
        }
    }
}
