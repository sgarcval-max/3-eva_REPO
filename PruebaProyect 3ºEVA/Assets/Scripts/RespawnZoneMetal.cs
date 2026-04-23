using UnityEngine;

public class RespawnZoneMetal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            MetalRespawn mr = other.GetComponent<MetalRespawn>();
            if (mr != null)
            {
                mr.DoRespawn();
            }
        }
    }
}
