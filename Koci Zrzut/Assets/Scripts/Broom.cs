using UnityEngine;

public class Broom : MonoBehaviour
{
    [Header("Faza ataku")]
    public float attackPhaseDuration = 1f;       // Czas trwania fazy ataku (ruch w g�r� i na boki)
    public float moveSpeed = 3f;                 // Pr�dko�� ruchu w fazie ataku
    public float attackRotationSpeed = 360f;     // Pr�dko�� obrotu podczas fazy ataku

    [Header("Faza opadania")]
    public float fallSpeed = 4f;                 // Pr�dko�� opadania
    public float fallRotationSpeed = 180f;         // Pr�dko�� obrotu podczas opadania

    private float spawnTime;
    private bool isFalling = false;
    private Vector2 moveDirection;               // Kierunek ruchu w fazie ataku

    void Start()
    {
        spawnTime = Time.time;
        // Ustal losowy, ale g��wnie pionowy kierunek (z lekkim dryblingiem horyzontalnym)
        float horizontal = Random.Range(-0.5f, 0.5f);
        moveDirection = new Vector2(horizontal, 1f).normalized;
    }

    void Update()
    {
        float elapsed = Time.time - spawnTime;

        if (!isFalling)
        {
            // Faza ataku � miot�a porusza si� w g�r� i na boki, obracaj�c si�
            transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, attackRotationSpeed * Time.deltaTime);

            // Po up�ywie attackPhaseDuration przejd� do fazy opadania
            if (elapsed >= attackPhaseDuration)
            {
                isFalling = true;
            }
        }
        else
        {
            // Faza opadania � miot�a porusza si� w d� i obraca (z mniejsz� pr�dko�ci�)
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, fallRotationSpeed * Time.deltaTime);
        }

        // Usuwamy miot��, je�li wysz�a poza doln� kraw�d� ekranu
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.y < 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Je�li miot�a dotknie gracza (np. kota), wywo�aj LoseLife() i usu� miot��
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>()?.LoseLife();
            Destroy(gameObject);
        }
    }
}
