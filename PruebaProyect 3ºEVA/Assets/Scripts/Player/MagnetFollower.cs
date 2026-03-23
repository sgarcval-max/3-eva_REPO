using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class MagnetFollower : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public Vector2 offset = new Vector2(0.5f, 0f);
    public Transform magnetTip;      // el imán completo
    public Transform magnetTipFace;  // punta activa del imán

    private Rigidbody2D rb;
    private float aimInput;
    public float rotateSpeed = 200f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = false;
    }

    void FixedUpdate()
    {
        if (playerRb != null)
        {
            Vector2 targetPos = playerRb.position + offset;
            rb.MovePosition(targetPos);
        }

        rb.MoveRotation(rb.rotation - aimInput * rotateSpeed * Time.fixedDeltaTime);
    }

    public void Aim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<float>();
    }
}