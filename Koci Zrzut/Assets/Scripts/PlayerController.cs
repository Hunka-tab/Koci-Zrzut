using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Ustawienia ruchu")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("¯ycie gracza")]
    public int currentLives = 3;
    public int maxLives = 5;
    public float invincibilityDuration = 2f;  // Czas nietykalnoœci po uderzeniu

    [Header("UI i Game Over")]
    public LifeUI lifeUI;
    public ScoreUI scoreUI;
    public GameObject gameOverScreen;  // Obiekt wyœwietlaj¹cy ekran przegranej

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded = false;
    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Aktualizacja UI na starcie
        if (lifeUI != null) lifeUI.UpdateLives(currentLives, maxLives);
        if (scoreUI != null) scoreUI.UpdateScore(0);
    }

    void Update()
    {
        if (currentLives <= 0) return; // Nie pozwalamy na ruch po œmierci

        // Odczytanie kierunku ruchu (strza³ki lub A/D)
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Animacja biegu
        animator.SetBool("isRunning", Mathf.Abs(move) > 0.01f);

        // Obrót gracza w stronê ruchu
        if (move > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0) transform.localScale = new Vector3(-1, 1, 1);

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Metoda odbieraj¹ca ¿ycie i uruchamiaj¹ca efekt migania
    public void LoseLife()
    {
        if (!isInvincible)
        {
            currentLives--;

            if (lifeUI != null) lifeUI.UpdateLives(currentLives, maxLives);

            if (currentLives <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(InvincibilityEffect());
            }
        }
    }

    // Miganie gracza po otrzymaniu obra¿eñ (nietykalnoœæ)
    IEnumerator InvincibilityEffect()
    {
        isInvincible = true;
        float elapsedTime = 0f;
        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Miganie
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.2f;
        }
        spriteRenderer.enabled = true;
        isInvincible = false;
    }

    // Metoda koñcz¹ca grê
    void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0f; // Pauza gry
    }

    // Metoda zwiêkszaj¹ca ¿ycie (np. po zebraniu rybki)
    public void GainLife(int amount)
    {
        currentLives = Mathf.Min(currentLives + amount, maxLives);
        if (lifeUI != null) lifeUI.UpdateLives(currentLives, maxLives);
    }

    // Metoda dodaj¹ca punkty
    public void AddScore(int value)
    {
        if (scoreUI != null) scoreUI.UpdateScore(value);
    }
}
