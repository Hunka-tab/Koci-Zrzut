using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencja do gracza
    public float offsetY = 2f; // Jak wysoko nad graczem ma byæ kamera
    public float smoothSpeed = 0.1f; // G³adkoœæ ruchu kamery

    void LateUpdate()
    {
        if (player != null)
        {
            // Nowa pozycja kamery – pod¹¿a tylko w górê
            Vector3 newPosition = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);

            // P³ynne przesuniêcie kamery (zamiast natychmiastowego skoku)
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }
    }
}
