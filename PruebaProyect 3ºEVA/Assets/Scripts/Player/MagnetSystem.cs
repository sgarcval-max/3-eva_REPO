using UnityEngine;

[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(Rigidbody2D))]
public class MagnetSystem : MonoBehaviour
{
    [Header("Magnet Settings")]
    public float magnetStrength = 15f;
    public float magnetRange = 6f;
    public float minDistance = 0.5f;
    public LayerMask metalLayer;

    private Rigidbody2D rb;
    private PlayerSetup setup;

    // Cache del objetivo actual
    private Rigidbody2D currentTarget;
    private MetalObject currentMetal;  // Cacheado para no llamar GetComponent cada frame

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        setup = GetComponent<PlayerSetup>();
    }

    void FixedUpdate()
    {
        if (currentTarget == null)
        {
            FindMetal();
        }
        else
        {
            AffectCurrentMetal();
        }
    }

    void FindMetal()
    {
        Collider2D[] metals = Physics2D.OverlapCircleAll(rb.position, magnetRange, metalLayer);

        float closestDistance = float.MaxValue;
        Rigidbody2D closestRb = null;
        MetalObject closestMetal = null;

        foreach (var col in metals)
        {
            Rigidbody2D metalRb = col.GetComponent<Rigidbody2D>();
            if (metalRb == null) continue;

            MetalObject metal = col.GetComponent<MetalObject>();
            if (metal == null) continue;

            float distance = Vector2.Distance(rb.position, col.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRb = metalRb;
                closestMetal = metal;
            }
        }

        // Asignamos el objetivo más cercano y cacheamos el MetalObject
        currentTarget = closestRb;
        currentMetal = closestMetal;
    }

    void AffectCurrentMetal()
    {
        if (currentTarget == null || currentMetal == null)
        {
            ReleaseCurrentMetal();
            return;
        }

        Vector2 myPos = rb.position;
        Vector2 targetPos = currentTarget.position;
        Vector2 dir = targetPos - myPos;
        float distance = dir.magnitude;

        // Soltar si se aleja demasiado
        if (distance > magnetRange * 1.2f)
        {
            ReleaseCurrentMetal();
            return;
        }

        if (distance < minDistance) return;

        // Fuerza más realista basada en distancia al cuadrado
        float strength = magnetStrength / (distance * distance);
        strength = Mathf.Clamp(strength, 0f, magnetStrength);
        Vector2 force = dir.normalized * strength;

        if (setup.isPositive != currentMetal.isPositive)
            currentTarget.AddForce(-force);  // Atracción
        else
            currentTarget.AddForce(force);   // Repulsión
    }

    public void ReleaseCurrentMetal()
    {
        currentTarget = null;
        currentMetal = null;  // Limpiamos también el cache
    }
}