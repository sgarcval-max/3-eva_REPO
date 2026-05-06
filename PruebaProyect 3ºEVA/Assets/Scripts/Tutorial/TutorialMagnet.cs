using UnityEngine;

public class TutorialMagnet : MonoBehaviour
{
    public TutorialHint hint;
    private MagnetSystem magnetSystem;
    private bool triggered = false;

    private void Awake()
    {
        magnetSystem = GetComponent<MagnetSystem>();
    }

    private void Update()
    {
        if (!triggered && magnetSystem.currentTarget != null)
        {
            triggered = true;
            hint.ShowHint();
        }
    }
}
