using UnityEngine;

public class MagneticMenuButton : MonoBehaviour
{
    public RectTransform magnet;        // El selector
    public float attractSpeed = 5f;     // Velocidad de movimiento
    public float offsetX = 100f;        // Distancia horizontal desde el selector
    public float maxRotation = 10f;     // Rotación máxima inicial en grados

    public MenuMagnetController menuController; // Referencia al controlador

    private Vector3 startPos;           // Posición original del botón
    private Quaternion startRot;        // Rotación inicial del botón
    public bool isSelected = false;     // Solo se mueve si está seleccionado

    void Start()
    {
        startPos = transform.position;

        // Rotación inicial aleatoria
        float randomAngle = Random.Range(-maxRotation, maxRotation);
        startRot = Quaternion.Euler(0, 0, randomAngle);
        transform.rotation = startRot;
    }

    void Update()
    {
        // --- NO hacemos nada hasta que el selector pueda interactuar ---
        if (menuController != null && !menuController.canSelect) return;

        if (isSelected)
        {
            // Posición deseada: al lado del selector
            Vector3 targetPos = new Vector3(magnet.position.x + offsetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * attractSpeed);

            // Rotación deseada: recto
            Quaternion targetRot = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * attractSpeed);
        }
        else
        {
            // Volver a posición original
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * attractSpeed);

            // Volver a rotación original
            transform.rotation = Quaternion.Lerp(transform.rotation, startRot, Time.deltaTime * attractSpeed);
        }
    }
}