using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public BgScroller bgScroller;        // referencia al fondo
    public GameObject[] levelPrefabs;    // prefabs de bloques
    public float blockWidth = 20f;       // ancho de cada bloque
    public int blocksInScene = 3;        // cuántos bloques activos

    private GameObject[] currentBlocks;

    void Start()
    {
        // Generamos los bloques iniciales
        currentBlocks = new GameObject[blocksInScene];
        for (int i = 0; i < blocksInScene; i++)
        {
            SpawnBlock(i, i * blockWidth);
        }
    }

    void Update()
    {
        if (bgScroller == null) return;

        // Solo movemos bloques si el jugador avanzó
        float advance = bgScroller.GetAdvanceAmount();
        Vector3 movement = bgScroller.horizontal ? Vector3.left : Vector3.down;

        for (int i = 0; i < blocksInScene; i++)
        {
            GameObject block = currentBlocks[i];
            block.transform.position += movement * advance;

            // Cuando el bloque sale de pantalla, reemplazamos
            if (bgScroller.horizontal)
            {
                if (block.transform.position.x <= -blockWidth)
                {
                    float maxX = GetMaxBlockX();
                    Destroy(block); // o usar pool
                    SpawnBlock(i, maxX + blockWidth);
                }
            }
            else
            {
                if (block.transform.position.y <= -blockWidth)
                {
                    float maxY = GetMaxBlockY();
                    Destroy(block);
                    SpawnBlock(i, maxY + blockWidth);
                }
            }
        }
    }

    void SpawnBlock(int index, float pos)
    {
        int rand = Random.Range(0, levelPrefabs.Length);
        GameObject newBlock = Instantiate(levelPrefabs[rand], new Vector3(pos, 0, 0), Quaternion.identity);
        currentBlocks[index] = newBlock;
    }

    float GetMaxBlockX()
    {
        float maxX = float.MinValue;
        foreach (GameObject b in currentBlocks)
            if (b.transform.position.x > maxX) maxX = b.transform.position.x;
        return maxX;
    }

    float GetMaxBlockY()
    {
        float maxY = float.MinValue;
        foreach (GameObject b in currentBlocks)
            if (b.transform.position.y > maxY) maxY = b.transform.position.y;
        return maxY;
    }
}