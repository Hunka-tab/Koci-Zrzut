using UnityEngine;
using System.Collections;

public class PlatformCleaner : MonoBehaviour
{
    public Transform player;  // Gracz
    public float platformDistanceLimit = 3f; // Dozwolona odleg³oœæ w dó³
    public float fallTimeToGameOver = 3f; // Czas spadania zanim nast¹pi Game Over

    private float lastPlatformY; // Pozycja Y ostatniej platformy
    private bool isFalling = false; // Czy licznik Game Over ju¿ dzia³a?

    void Start()
    {
        lastPlatformY = player.position.y; // Pocz¹tkowa platforma
    }

    void Update()
    {
        // Obliczamy dozwolony limit wysokoœci spadania
        float lowestAllowedY = lastPlatformY - (platformDistanceLimit * 2f);

        // Jeœli gracz spad³ poni¿ej tej granicy, uruchamiamy Game Over
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

        // Jeœli gracz nadal spada, koñczymy grê
        if (player.position.y < lastPlatformY - (platformDistanceLimit * 2f))
        {
            Debug.Log("GAME OVER!");
            Time.timeScale = 0; // Zatrzymanie gry
        }

        isFalling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Jeœli gracz dotkn¹³ platformy, aktualizujemy ostatni¹ dobr¹ pozycjê
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastPlatformY = player.position.y;
            isFalling = false; // Zatrzymujemy licznik Game Over
        }
    }
}
