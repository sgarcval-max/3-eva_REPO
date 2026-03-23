using UnityEngine;

public class MoveWithBg : MonoBehaviour
{
    public BgScroller bgScroller; // arrastra Bg1
    private Vector3 lastBgPos;

    void Start()
    {
        if (bgScroller != null)
            lastBgPos = bgScroller.transform.position;
    }

    void Update()
    {
        if (bgScroller == null) return;

        // cuánto se ha movido el fondo desde el frame anterior
        Vector3 delta = bgScroller.transform.position - lastBgPos;

        // mueve este bloque exactamente la misma distancia
        transform.position += delta;

        lastBgPos = bgScroller.transform.position;
    }
}