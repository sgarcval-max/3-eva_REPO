using UnityEngine;

public class TutorialMovement : MonoBehaviour
{
    public TutorialHint hint;
    private bool triggered = false;

    private void Update()
    {
        if (!triggered && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)
            || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            triggered = true;
            hint.ShowHint();
        }
    }
}