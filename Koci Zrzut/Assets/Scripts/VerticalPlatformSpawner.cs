using UnityEngine;

public class VerticalPlatformSpawner : MonoBehaviour
{
    [Header("Ustawienia spawnu platform (vertical)")]
    public GameObject[] platformPrefabs;   // Przypisz tutaj prefabry platform (np. Platform_1, Platform_2)
    public float spawnDistance = 10f;        // Odleg�o�� w g�r� od gracza, do kt�rej generowane s� nowe platformy
    public float minGap = 1f;                // Minimalna przerwa pionowa mi�dzy platformami
    public float maxGap = 3f;                // Maksymalna przerwa pionowa mi�dzy platformami
    public float minX = -2f;                 // Minimalna pozycja X, na kt�rej mo�e pojawi� si� platforma
    public float maxX = 2f;                  // Maksymalna pozycja X, na kt�rej mo�e pojawi� si� platforma

    [Header("Referencja do gracza")]
    public Transform player;

    private float nextSpawnY = 0f;           // Pozycja Y, gdzie pojawi si� kolejna platforma

    void Start()
    {
        // Na starcie ustawiamy nextSpawnY na aktualnej pozycji gracza (lub wy�ej, je�li chcesz)
        nextSpawnY = player.position.y;
    }

    void Update()
    {
        // Je�eli gracz zbli�a si� (w pionie) do granicy, gdzie jeszcze nie ma platform, generujemy kolejn�
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

        // Losujemy pozycj� X w zakresie minX - maxX
        float randomX = Random.Range(minX, maxX);

        // Ustawiamy pozycj� spawn'u � na wysoko�ci nextSpawnY i w losowej pozycji X
        Vector3 spawnPos = new Vector3(randomX, nextSpawnY, 0);

        // Tworzymy platform�
        GameObject newPlatform = Instantiate(platformPrefab, spawnPos, Quaternion.identity);

        // Pr�ba odczytania wysoko�ci platformy z komponentu SpriteRenderer
        float platformHeight = 1f;
        SpriteRenderer sr = newPlatform.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            platformHeight = sr.bounds.size.y;
        }

        // Zwi�kszamy nextSpawnY � wysoko�� platformy plus losowa przerwa
        nextSpawnY += platformHeight + Random.Range(minGap, maxGap);
    }
}
