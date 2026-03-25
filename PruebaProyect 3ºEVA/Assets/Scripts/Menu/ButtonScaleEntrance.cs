using UnityEngine;

public class ButtonScaleEntrance : MonoBehaviour
{
    public float duration = 1f;     // Duración de la animación
    public float startScale = 0.5f; // Escala inicial

    private Vector3 targetScale;
    private float timer = 0f;

    [HideInInspector] public bool isFinished = false; // ← Esta variable indica cuando termina

    void Start()
    {
        targetScale = transform.localScale;
        transform.localScale = targetScale * startScale;
    }

    void Update()
    {
        if (!isFinished)
        {
            timer += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, timer / duration);
            transform.localScale = Vector3.Lerp(targetScale * startScale, targetScale, t);

            if (t >= 1f)
            {
                transform.localScale = targetScale; // Aseguramos que termine exacto
                isFinished = true;                  // Marcamos como terminado
            }
        }
    }
}
