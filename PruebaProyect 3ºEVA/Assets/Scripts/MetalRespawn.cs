using UnityEngine;

public class MetalRespawn : MonoBehaviour
{
    public Transform respawnPoint;  // Cada objeto Metal tiene el suyo

    public void DoRespawn()
    {
        transform.position = respawnPoint.position;
    }
}
