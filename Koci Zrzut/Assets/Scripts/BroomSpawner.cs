using UnityEngine;

public class BroomSpawner : MonoBehaviour
{
    public GameObject broomPrefab;
    public float spawnIntervalMin = 30f;
    public float spawnIntervalMax = 90f;
    public Transform player;

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBroom();
            ScheduleNextSpawn();
        }
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void SpawnBroom()
    {
        Vector3 spawnPosition;
        int side = Random.Range(0, 2);

        if (side == 0) // Spawnowanie z do³u ekranu
        {
            spawnPosition = new Vector3(player.position.x + Random.Range(-3f, 3f), player.position.y - 5f, 0);
        }
        else // Spawnowanie z boku ekranu
        {
            float xOffset = Random.Range(0, 2) == 0 ? -7f : 7f;
            spawnPosition = new Vector3(player.position.x + xOffset, player.position.y, 0);
        }

        Instantiate(broomPrefab, spawnPosition, Quaternion.identity);
    }
}
