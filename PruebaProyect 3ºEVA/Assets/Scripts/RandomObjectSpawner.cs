using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] metalObjects;
    public Transform[] spawnPoints;

    public float minTime = 3f;
    public float maxTime = 6f;

    void Start()
    {
        Invoke("SpawnObject", Random.Range(minTime, maxTime));
    }

    void SpawnObject()
    {
        int obj = Random.Range(0, metalObjects.Length);
        int spawn = Random.Range(0, spawnPoints.Length);

        Instantiate(metalObjects[obj], spawnPoints[spawn].position, Quaternion.identity);

        Invoke("SpawnObject", Random.Range(minTime, maxTime));
    }
}
