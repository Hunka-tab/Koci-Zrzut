using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public int scoreValue = 1;  // Punkty za str¹cenie
    public float fallSpeed = 5f;
    private bool isFalling = false;

    void Update()
    {
        if (isFalling)
        {
            // Poruszaj siê w dó³
            transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    // Gdy gracz wejdzie w trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFalling && collision.CompareTag("Player"))
        {
            isFalling = true;
            // Dodaj punkty
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.AddScore(scoreValue);
            }
        }
    }
}
