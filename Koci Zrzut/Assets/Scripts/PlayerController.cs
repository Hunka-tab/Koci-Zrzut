using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    public float speed = 5f;       // Prêdkoœæ poruszania w poziomie
    public float jumpForce = 7f;   // Si³a skoku

    [Header("Wynik i ¿ycie")]
    public int score = 0;
    public int currentLives = 3;
    public int maxLives = 5;

    // Referencje do skryptów UI (przypisz je w Inspectorze)
    public LifeUI lifeUI;        // Skrypt, który aktualizuje ikony ¿ycia
    public ScoreUI scoreUI;      // Skrypt, który aktualizuje wyœwietlanie punktów

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Aktualizujemy UI na starcie
        if (lifeUI != null)
            lifeUI.UpdateLives(currentLives, maxLives);
        if (scoreUI != null)
            scoreUI.UpdateScore(score);
    }

    void Update()
    {
        // Odczytanie kierunku ruchu (strza³ki lub A/D)
        float move = Input.GetAxis("Horizontal");

        // Ruch poziomy
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Ustawianie parametru "isRunning" w Animatorze
        bool running = Mathf.Abs(move) > 0.01f;
        animator.SetBool("isRunning", running);

        // Obracanie postaci w stronê ruchu
        if (move > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Skok (klawisz Space / przycisk "Jump" w Input Manager)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    // Wykrywanie kolizji z pod³o¿em (upewnij siê, ¿e pod³o¿e ma tag "Ground")
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Metoda dodaj¹ca punkty
    public void AddScore(int value)
    {
        score += value;
        if (scoreUI != null)
            scoreUI.UpdateScore(score);
        Debug.Log("Score: " + score);
    }

    // Metoda zwiêkszaj¹ca ¿ycie (np. po zebraniu rybki)
    public void GainLife(int amount)
    {
        currentLives = Mathf.Min(currentLives + amount, maxLives);
        if (lifeUI != null)
            lifeUI.UpdateLives(currentLives, maxLives);
        Debug.Log("Lives: " + currentLives);
    }

    // Metoda odejmuj¹ca ¿ycie (np. po otrzymaniu obra¿eñ)
    public void TakeDamage(int damage)
    {
        currentLives -= damage;
        if (currentLives < 0) currentLives = 0;
        if (lifeUI != null)
            lifeUI.UpdateLives(currentLives, maxLives);
        Debug.Log("Lives: " + currentLives);
    }
}
