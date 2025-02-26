using UnityEngine;
using System.Collections;

public class PlatformCleaner : MonoBehaviour
{
    public Transform player;  // Gracz
    public float platformDistanceLimit = 3f; // Dozwolona odleg�o�� w d�
    public float fallTimeToGameOver = 3f; // Czas spadania zanim nast�pi Game Over

    private float lastPlatformY; // Pozycja Y ostatniej platformy
    private bool isFalling = false; // Czy licznik Game Over ju� dzia�a?

    void Start()
    {
        lastPlatformY = player.position.y; // Pocz�tkowa platforma
    }

    void Update()
    {
        // Obliczamy dozwolony limit wysoko�ci spadania
        float lowestAllowedY = lastPlatformY - (platformDistanceLimit * 2f);

        // Je�li gracz spad� poni�ej tej granicy, uruchamiamy Game Over
        if (player.position.y < lowestAllowedY && !isFalling)
        {
            StartCoroutine(GameOverCountdown());
        }

        // Usuwanie starych platform
        foreach (GameObject platform in GameObject.FindGameObjectsWithTag("Ground"))
        {
            if (platform.transform.position.y < lowestAllowedY)
            {
                Destroy(platform);
            }
        }
    }

    IEnumerator GameOverCountdown()
    {
        isFalling = true; // Uruchamiamy licznik
        yield return new WaitForSeconds(fallTimeToGameOver);

        // Je�li gracz nadal spada, ko�czymy gr�
        if (player.position.y < lastPlatformY - (platformDistanceLimit * 2f))
        {
            Debug.Log("GAME OVER!");
            Time.timeScale = 0; // Zatrzymanie gry
        }

        isFalling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Je�li gracz dotkn�� platformy, aktualizujemy ostatni� dobr� pozycj�
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastPlatformY = player.position.y;
            isFalling = false; // Zatrzymujemy licznik Game Over
        }
    }
}
