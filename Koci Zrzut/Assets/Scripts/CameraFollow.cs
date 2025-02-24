using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencja do gracza
    public float offsetY = 2f; // Jak wysoko nad graczem ma by� kamera
    public float smoothSpeed = 0.1f; // G�adko�� ruchu kamery

    void LateUpdate()
    {
        if (player != null)
        {
            // Nowa pozycja kamery � pod��a tylko w g�r�
            Vector3 newPosition = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);

            // P�ynne przesuni�cie kamery (zamiast natychmiastowego skoku)
            transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed);
        }
    }
}
