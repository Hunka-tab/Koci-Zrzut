using UnityEngine;

public class VerticalPlatformSpawner : MonoBehaviour
{
    [Header("Ustawienia spawnu platform (vertical)")]
    public GameObject[] platformPrefabs;   // Przypisz tutaj prefabry platform (np. Platform_1, Platform_2)
    public float spawnDistance = 10f;        // Odleg³oœæ w górê od gracza, do której generowane s¹ nowe platformy
    public float minGap = 1f;                // Minimalna przerwa pionowa miêdzy platformami
    public float maxGap = 3f;                // Maksymalna przerwa pionowa miêdzy platformami
    public float minX = -2f;                 // Minimalna pozycja X, na której mo¿e pojawiæ siê platforma
    public float maxX = 2f;                  // Maksymalna pozycja X, na której mo¿e pojawiæ siê platforma

    [Header("Referencja do gracza")]
    public Transform player;

    private float nextSpawnY = 0f;           // Pozycja Y, gdzie pojawi siê kolejna platforma

    void Start()
    {
        // Na starcie ustawiamy nextSpawnY na aktualnej pozycji gracza (lub wy¿ej, jeœli chcesz)
        nextSpawnY = player.position.y;
    }

    void Update()
    {
        // Je¿eli gracz zbli¿a siê (w pionie) do granicy, gdzie jeszcze nie ma platform, generujemy kolejn¹
        if (player.position.y + spawnDistance > nextSpawnY)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        // Losujemy prefab platformy
        int randIndex = Random.Range(0, platformPrefabs.Length);
        GameObject platformPrefab = platformPrefabs[randIndex];

        // Losujemy pozycjê X w zakresie minX - maxX
        float randomX = Random.Range(minX, maxX);

        // Ustawiamy pozycjê spawn'u – na wysokoœci nextSpawnY i w losowej pozycji X
        Vector3 spawnPos = new Vector3(randomX, nextSpawnY, 0);

        // Tworzymy platformê
        GameObject newPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        // Próba odczytania wysokoœci platformy z komponentu SpriteRenderer
        float platformHeight = 1f;
        SpriteRenderer sr = newPlatform.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            platformHeight = sr.bounds.size.y;
        }

        // Zwiêkszamy nextSpawnY – wysokoœæ platformy plus losowa przerwa
        nextSpawnY += platformHeight + Random.Range(minGap, maxGap);
    }
}
