using UnityEngine;

public class Broom : MonoBehaviour
{
    [Header("Faza ataku")]
    public float attackPhaseDuration = 1f;       // Czas trwania fazy ataku (ruch w górê i na boki)
    public float moveSpeed = 3f;                 // Prêdkoœæ ruchu w fazie ataku
    public float attackRotationSpeed = 360f;     // Prêdkoœæ obrotu podczas fazy ataku

    [Header("Faza opadania")]
    public float fallSpeed = 4f;                 // Prêdkoœæ opadania
    public float fallRotationSpeed = 180f;         // Prêdkoœæ obrotu podczas opadania

    private float spawnTime;
    private bool isFalling = false;
    private Vector2 moveDirection;               // Kierunek ruchu w fazie ataku

    void Start()
    {
        spawnTime = Time.time;
        // Ustal losowy, ale g³ównie pionowy kierunek (z lekkim dryblingiem horyzontalnym)
        float horizontal = Random.Range(-0.5f, 0.5f);
        moveDirection = new Vector2(horizontal, 1f).normalized;
    }

    void Update()
    {
        float elapsed = Time.time - spawnTime;

        if (!isFalling)
        {
            // Faza ataku – miot³a porusza siê w górê i na boki, obracaj¹c siê
            transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, attackRotationSpeed * Time.deltaTime);

            // Po up³ywie attackPhaseDuration przejdŸ do fazy opadania
            if (elapsed >= attackPhaseDuration)
            {
                isFalling = true;
            }
        }
        else
        {
            // Faza opadania – miot³a porusza siê w dó³ i obraca (z mniejsz¹ prêdkoœci¹)
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, fallRotationSpeed * Time.deltaTime);
        }

        // Usuwamy miot³ê, jeœli wysz³a poza doln¹ krawêdŸ ekranu
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.y < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Jeœli miot³a dotknie gracza (np. kota), wywo³aj LoseLife() i usuñ miot³ê
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.LoseLife();
            Destroy(gameObject);
        }
    }
}
