using UnityEngine;
using UnityEngine.SceneManagement;

public class LethalMagnetBox : MonoBehaviour
{
    [Header("Magnet Settings")]
    public float magnetStrength = 50f;
    public float magnetRange = 6f; // distancia máxima de atracción
    public float magnetForceMultiplier = 0.5f;

    public bool isPositive = true; // este MetalBox tiene polaridad
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        AttractOppositePlayers();
    }

    void AttractOppositePlayers()
    {
        // Buscar todos los jugadores
        PlayerSetup[] players = FindObjectsOfType<PlayerSetup>();

        foreach (PlayerSetup player in players)
        {
            // Solo atraer si el jugador tiene polo OPUESTO
            if (player.isPositive == isPositive)
                continue; // mismo polo → no atrae

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb == null) continue;

            Vector2 direction = transform.position - player.transform.position;
            float distance = direction.magnitude;

            if (distance > magnetRange) continue; // fuera de rango

            direction.Normalize();
            float distanceFactor = Mathf.Clamp01(1f - (distance / magnetRange));

            playerRb.AddForce(direction * magnetStrength * magnetForceMultiplier * distanceFactor);
        }
    }

    // Detecta si el jugador toca este MetalBox
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerSetup player = collision.collider.GetComponent<PlayerSetup>();
        if (player != null)
        {
            Debug.Log("Jugador ha muerto por tocar el MetalBox letal!");

            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
