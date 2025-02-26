using UnityEngine;
using System.Collections;

public class BroomMovement : MonoBehaviour
{
    public float moveSpeed = 3f;       // Szybkoœæ poruszania siê miot³y
    public float attackCooldown = 2f;  // Czas miêdzy atakami
    public float attackForce = 5f;     // Si³a odrzutu po trafieniu

    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AttackCycle());
    }

    void Update()
    {
        if (!isAttacking && player != null)
        {
            // Miot³a pod¹¿a za graczem w poziomie
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown);
            if (player != null)
            {
                // Miot³a wykonuje zamach w stronê gracza
                isAttacking = true;
                Vector3 attackPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
                float attackTime = 0.5f;
                float elapsedTime = 0f;

                Vector3 startPos = transform.position;

                while (elapsedTime < attackTime)
                {
                    transform.position = Vector3.Lerp(startPos, attackPosition, elapsedTime / attackTime);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Powrót do pierwotnej pozycji po ataku
                isAttacking = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().LoseLife();
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 knockback = new Vector2(Random.Range(-1f, 1f), 1f) * attackForce;
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }
        }
    }
}
