using UnityEngine;

public class ResetGeneratorZone : MonoBehaviour
{
    public LiftPlatform platform;
    public BrokenGenerator brokenGenerator;  // Referencia al BrokenGenerator

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            platform.ResetMission();
            if (brokenGenerator != null)
                brokenGenerator.ResetGenerator();
            Debug.Log("Generador reseteado, vuelve a funcionar");
        }
    }
}
