using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 2f; // Pr�dko�� przesuwania t�a
    public float backgroundHeight; // Wysoko�� t�a (ustaw na wysoko�� sprite'a)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Gdy t�o przesunie si� w g�r� o wysoko�� jednego obrazka, resetujemy pozycj�
        if (transform.position.y >= startPosition.y + backgroundHeight)
        {
            transform.position -= new Vector3(0, backgroundHeight * 2, 0);
        }
    }
}
