using UnityEngine;

public class BgScroller : MonoBehaviour
{
    [Header("Velocidad máxima del fondo/suelo")]
    public float maxSpeed = 3f;

    [Header("Segundo fondo/suelo para loop")]
    public Transform secondBg;

    [Header("Jugadores")]
    public Transform player1;
    public Transform player2;

    [Header("Movimiento horizontal (true) o vertical (false)")]
    public bool horizontal = true;

    [Header("Punto donde empieza a moverse")]
    public float activationPointX = 5f;

    [HideInInspector] public Vector3 lastPlayer1Pos;
    [HideInInspector] public Vector3 lastPlayer2Pos;

    private float spriteWidth;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        lastPlayer1Pos = player1.position;
        lastPlayer2Pos = player2.position;
    }

    void Update()
    {
        float advance = GetAdvanceAmount();
        Vector3 movement = horizontal ? Vector3.left : Vector3.down;

        transform.position += movement * advance;
        if (secondBg != null)
            secondBg.position += movement * advance;

        // Loop fondo
        if (horizontal)
        {
            if (transform.position.x <= startPos.x - spriteWidth)
                transform.position = secondBg.position + Vector3.right * spriteWidth;
            if (secondBg.position.x <= startPos.x - spriteWidth)
                secondBg.position = transform.position + Vector3.right * spriteWidth;
        }
        else
        {
            if (transform.position.y <= startPos.y - spriteWidth)
                transform.position = secondBg.position + Vector3.up * spriteWidth;
            if (secondBg.position.y <= startPos.y - spriteWidth)
                secondBg.position = transform.position + Vector3.up * spriteWidth;
        }
    }

    // Devuelve cuánto se debe mover fondo
    public float GetAdvanceAmount()
    {
        float advance = 0f;
        float frontPlayerX = Mathf.Max(player1.position.x, player2.position.x);

        if (frontPlayerX >= activationPointX)
        {
            if (horizontal)
            {
                if (player1.position.x > lastPlayer1Pos.x) advance = player1.position.x - lastPlayer1Pos.x;
                if (player2.position.x > lastPlayer2Pos.x) advance = Mathf.Max(advance, player2.position.x - lastPlayer2Pos.x);
            }
            else
            {
                if (player1.position.y > lastPlayer1Pos.y) advance = player1.position.y - lastPlayer1Pos.y;
                if (player2.position.y > lastPlayer2Pos.y) advance = Mathf.Max(advance, player2.position.y - lastPlayer2Pos.y);
            }
        }

        lastPlayer1Pos = player1.position;
        lastPlayer2Pos = player2.position;
        return advance * maxSpeed;
    }
}