using UnityEngine;

public class LevelBlock : MonoBehaviour
{
    public BgScroller bgScroller; // referencia al script del fondo
    public float blockWidth = 20f;

    void Update()
    {
        // Se mueve con el fondo
        Vector3 movement = bgScroller.horizontal ? Vector3.left : Vector3.down;
        float advance = 0f;

        // Calculamos avance usando el avance del fondo
        if (bgScroller.horizontal)
            advance = bgScroller.maxSpeed * Time.deltaTime;
        else
            advance = bgScroller.maxSpeed * Time.deltaTime;

        transform.position += movement * advance;

        // Loop: cuando sale de la pantalla lo movemos al final
        if (bgScroller.horizontal)
        {
            if (transform.position.x <= -blockWidth)
            {
                transform.position += Vector3.right * blockWidth * 2f;
                RandomizeBlock(); // cambia su contenido para dar variedad
            }
        }
        else
        {
            if (transform.position.y <= -blockWidth)
            {
                transform.position += Vector3.up * blockWidth * 2f;
                RandomizeBlock();
            }
        }
    }

    void RandomizeBlock()
    {
        // Aquí puedes activar/desactivar paredes, imanes, plataformas
        // Ejemplo: cada vez que loop, activa o desactiva objetos hijos
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(Random.value > 0.5f);
        }
    }
}
