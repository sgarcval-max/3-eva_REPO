using UnityEngine;

[RequireComponent(typeof(PlayerSetup))]
[RequireComponent(typeof(Rigidbody2D))]
public class MagnetSystem : MonoBehaviour
{
    [Header("Magnet Settings")]
    public float magnetStrength = 15f;
    public float magnetRange = 6f;
    public float minDistance = 0.5f;
    public LayerMask metalLayer;  // Solo objetos metálicos

    private Rigidbody2D rb;
    private PlayerSetup setup;

    // NUEVO: objeto que está siendo atraído
    private Rigidbody2D currentTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        setup = GetComponent<PlayerSetup>();
    }

    void FixedUpdate()
    {
        if (currentTarget == null)
        {
            // Buscar un nuevo metal si no hay ninguno siendo atraído
            FindMetal();
        }
        else
        {
            // Seguir atrayendo o repeliendo el objeto actual
            AffectCurrentMetal();
        }
    }

    void FindMetal()
    {
        Collider2D[] metals = Physics2D.OverlapCircleAll(rb.position, magnetRange, metalLayer);

        foreach (var col in metals)
        {
            Rigidbody2D metalRb = col.GetComponent<Rigidbody2D>();
            if (metalRb == null) continue;

            MetalObject metal = col.GetComponent<MetalObject>();
            if (metal == null) continue;

            // Solo selecciona el primer objeto que cumpla las condiciones
            currentTarget = metalRb;
            break;
        }
    }

    void AffectCurrentMetal()
    {
        if (currentTarget == null) return;

        Vector2 dir = currentTarget.position - rb.position;
        float distance = dir.magnitude;

        // Soltar si se aleja demasiado
        if (distance > magnetRange * 1.2f)
        {
            currentTarget = null;
            return;
        }

        // Aplicar fuerza
        MetalObject metal = currentTarget.GetComponent<MetalObject>();
        if (metal == null)
        {
            currentTarget = null;
            return;
        }

        if (distance < minDistance) return;

        float strength = magnetStrength * (1f - distance / magnetRange);
        Vector2 force = dir.normalized * strength;

        if (setup.isPositive != metal.isPositive)
            currentTarget.AddForce(-force); // Atracción
        else
            currentTarget.AddForce(force);  // Repulsión
    }

    // NUEVO: Método público para soltar el objeto
    public void ReleaseCurrentMetal()
    {
        currentTarget = null;
    }
}