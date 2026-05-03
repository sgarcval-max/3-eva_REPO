using UnityEngine;

public class ResetGeneratorZone : MonoBehaviour
{
    public LiftPlatform platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platform.ResetMission();
            Debug.Log("Generador reseteado, vuelve a funcionar");
        }
    }
}
