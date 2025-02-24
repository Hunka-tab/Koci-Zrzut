using UnityEngine;

public class ItemSpawnerOnPlatform : MonoBehaviour
{
    public GameObject[] items;   // Prefaby przedmiotów
    [Range(0, 1f)] public float spawnChance = 0.5f; // Szansa na pojawienie siê przedmiotu
    public float itemSpawnOffsetY = 0.5f; // Wysokoœæ spawnu przedmiotu nad platform¹
    public Vector3 itemScale = new Vector3(1f, 1f, 1f); // Wymuszona skala przedmiotów

    void Start()
    {
        if (Random.value < spawnChance && items.Length > 0)
        {
            // Losowy item z tablicy
            int randIndex = Random.Range(0, items.Length);
            GameObject itemPrefab = items[randIndex];

            // Pobieramy Collider platformy, by obliczyæ jej wymiary
            Collider2D platformCollider = GetComponent<Collider2D>();

            if (platformCollider != null)
            {
                // Obliczamy szerokoœæ platformy
                float platformWidth = platformCollider.bounds.size.x;

                // Losowa pozycja w poziomie (w granicach platformy)
                float randomX = Random.Range(-platformWidth / 2f + 0.3f, platformWidth / 2f - 0.3f);

                // Pozycja spawnu przedmiotu (nad platform¹)
                Vector3 spawnPos = new Vector3(transform.position.x + randomX,
                                               transform.position.y + platformCollider.bounds.extents.y + itemSpawnOffsetY,
                                               transform.position.z);

                // Tworzymy przedmiot i ustawiamy jego poprawn¹ skalê
                GameObject spawnedItem = Instantiate(itemPrefab, spawnPos, Quaternion.identity, transform);
                spawnedItem.transform.localScale = itemScale; // Wymuszenie poprawnej skali
            }
        }
    }
}
