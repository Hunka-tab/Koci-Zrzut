using UnityEngine;

public class Broom : MonoBehaviour
{
    public float speed = 5f; // Pr�dko�� miot�y
    public float lifeTime = 5f; // Jak d�ugo miot�a istnieje
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, lifeTime); // Usuwa miot�� po czasie
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
            Destroy(gameObject); // Usuwa miot�� po trafieniu gracza
        }
    }
}
