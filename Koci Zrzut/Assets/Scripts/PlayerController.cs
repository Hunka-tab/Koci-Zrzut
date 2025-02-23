using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    public float speed = 5f;       // Prêdkoœæ poruszania w poziomie
    public float jumpForce = 7f;   // Si³a skoku

    private Rigidbody2D rb;        // Referencja do Rigidbody2D
    private Animator animator;     // Referencja do Animatora
    private bool isGrounded = false; // Czy gracz stoi na pod³o¿u

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Odczytanie kierunku ruchu (strza³ki lub A/D)
        float move = Input.GetAxis("Horizontal");

        // Ruch poziomy
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Ustawianie parametru "isRunning" w Animatorze
        // true, gdy |move| > 0, w przeciwnym razie false
        bool running = Mathf.Abs(move) > 0.01f;
        animator.SetBool("isRunning", running);

        // Obracanie postaci w stronê ruchu
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Skok (klawisz Space / przycisk "Jump" w Input Manager)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // Wykrywanie kolizji z pod³o¿em
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Za³ó¿my, ¿e tag "Ground" maj¹ platformy/pod³o¿e
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
