using UnityEngine;

public class BrokenGenerator : MonoBehaviour
{
    public LiftPlatform platform;

    private bool repaired = false;

    public void Repair()
    {
        repaired = true;
    }

    public void GiveEnergy()
    {
        if (repaired)
        {
            platform.ActivateLift();
        }
    }
}
