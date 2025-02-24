using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Ustawienia spawnu platform")]
    public GameObject[] platformPrefabs;   // Przypisz tutaj prefaby platform
    public float spawnDistance = 10f;        // Odleg³oœæ przed graczem, w której maj¹ siê pojawiaæ nowe platformy
    public float minY = -1f;                 // Minimalna wysokoœæ spawn'u platformy
    public float maxY = 2f;                  // Maksymalna wysokoœæ spawn'u platformy
    public float minGap = 1f;                // Minimalna przerwa miêdzy platformami
    public float maxGap = 3f;                // Maksymalna przerwa miêdzy platformami

    [Header("Referencja do gracza")]
    public Transform player;

    private float nextSpawnX = 0f;           // Pozycja X, gdzie pojawi siê kolejna platforma

    void Update()
    {
        // Jeœli gracz jest blisko koñca wygenerowanych platform, spawnujemy kolejn¹
        if (player.position.x + spawnDistance > nextSpawnX)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        int randIndex = Random.Range(0, platformPrefabs.Length);
        GameObject platformPrefab = platformPrefabs[randIndex];

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(nextSpawnX, randomY, 0);

        GameObject newPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        // Oblicz szerokoœæ platformy na podstawie komponentu SpriteRenderer
        float platformWidth = newPlatform.GetComponent<SpriteRenderer>().bounds.size.x;

        // Ustaw kolejn¹ pozycjê spawn'u – szerokoœæ platformy + losowa przerwa
        nextSpawnX += platformWidth + Random.Range(minGap, maxGap);
    }
}
