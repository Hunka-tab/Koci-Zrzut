using UnityEngine;

public class FishPickup : MonoBehaviour
{
    public int lifeGain = 1; // Ile ¿ycia daje rybka

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.GainLife(lifeGain);
            }
            Destroy(gameObject);
        }
    }
}
