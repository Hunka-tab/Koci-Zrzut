using UnityEngine;

public class Broom : MonoBehaviour
{
    public float speed = 5f; // Prêdkoœæ miot³y
    public float lifeTime = 5f; // Jak d³ugo miot³a istnieje
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, lifeTime); // Usuwa miot³ê po czasie
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.LoseLife();
            }
            Destroy(gameObject); // Usuwa miot³ê po trafieniu gracza
        }
    }
}
