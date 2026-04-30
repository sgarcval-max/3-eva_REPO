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

    [Header("Particle System")]
    public ParticleSystem beamParticles;

    [Header("Referencias")]
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;
    private PlayerSetup setup;

    public Rigidbody2D currentTarget;
    private MetalObject currentMetal;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        setup = GetComponent<PlayerSetup>();

        if (beamParticles != null)
            beamParticles.Stop();
    }

    void FixedUpdate()
    {
        if (currentTarget == null)
        {
            FindMetal();
            StopBeam();
            if (playerMovement != null)
                playerMovement.SetMagnetAnimation(false);
        }
        else
        {
            AffectCurrentMetal();
            if (playerMovement != null)
                playerMovement.SetMagnetAnimation(true);
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

        if (distance > magnetRange * 1.2f)
        {
            ReleaseCurrentMetal();
            return;
        }

        if (distance < minDistance)
        {
            StopBeam();
            return;
        }

        UpdateBeam(myPos, targetPos);

        float strength = magnetStrength / (distance * distance);
        strength = Mathf.Clamp(strength, 0f, magnetStrength);
        Vector2 force = dir.normalized * strength;

        if (setup.isPositive != currentMetal.isPositive)
            currentTarget.AddForce(-force);
        else
            currentTarget.AddForce(force);
    }

    void UpdateBeam(Vector2 from, Vector2 to)
    {
        if (beamParticles == null) return;

        beamParticles.transform.position = from;

        Vector2 dir = (to - from).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        beamParticles.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        float distance = Vector2.Distance(from, to);
        var main = beamParticles.main;
        main.startLifetime = distance / main.startSpeed.constant;

        if (!beamParticles.isPlaying)
            beamParticles.Play();
    }

    void StopBeam()
    {
        if (beamParticles != null && beamParticles.isPlaying)
            beamParticles.Stop();
    }

    public void ReleaseCurrentMetal()
    {
        currentTarget = null;
        currentMetal = null;
        StopBeam();
        if (playerMovement != null)
            playerMovement.SetMagnetAnimation(false);
    }
}