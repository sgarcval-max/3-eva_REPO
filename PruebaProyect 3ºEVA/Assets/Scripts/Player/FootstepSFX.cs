using UnityEngine;

public class FootstepSFX : MonoBehaviour
{
    public float stepInterval = 0.4f;
    private float stepTimer = 0f;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float speed = Mathf.Abs(rb.linearVelocity.x);

        if (playerMovement.isGrounded && speed > 0.1f)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                AudioManager.instance.PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
