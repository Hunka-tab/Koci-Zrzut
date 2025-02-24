using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Ustawienia spawnu platform")]
    public GameObject[] platformPrefabs;   // Przypisz tutaj prefaby platform
    public float spawnDistance = 10f;        // Odleg�o�� przed graczem, w kt�rej maj� si� pojawia� nowe platformy
    public float minY = -1f;                 // Minimalna wysoko�� spawn'u platformy
    public float maxY = 2f;                  // Maksymalna wysoko�� spawn'u platformy
    public float minGap = 1f;                // Minimalna przerwa mi�dzy platformami
    public float maxGap = 3f;                // Maksymalna przerwa mi�dzy platformami

    [Header("Referencja do gracza")]
    public Transform player;

    private float nextSpawnX = 0f;           // Pozycja X, gdzie pojawi si� kolejna platforma

    void Update()
    {
        // Je�li gracz jest blisko ko�ca wygenerowanych platform, spawnujemy kolejn�
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

        // Oblicz szeroko�� platformy na podstawie komponentu SpriteRenderer
        float platformWidth = newPlatform.GetComponent<SpriteRenderer>().bounds.size.x;

        // Ustaw kolejn� pozycj� spawn'u � szeroko�� platformy + losowa przerwa
        nextSpawnX += platformWidth + Random.Range(minGap, maxGap);
    }
}
