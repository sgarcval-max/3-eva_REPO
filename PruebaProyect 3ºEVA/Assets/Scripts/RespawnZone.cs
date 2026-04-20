using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [Header("Respawn")]
    public Transform respawnPointPlayer1;
    public Transform respawnPointPlayer2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.name == "Player1")
            {
                other.transform.position = respawnPointPlayer1.position;
            }
            else if (other.gameObject.name == "Player2")
            {
                other.transform.position = respawnPointPlayer2.position;
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Metal"))
        {
            MetalRespawn mr = other.GetComponent<MetalRespawn>();
            if (mr != null)
            {
                mr.DoRespawn();
            }
        }
    }
}
