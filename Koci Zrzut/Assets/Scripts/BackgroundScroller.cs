using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 2f; // Prêdkoœæ przesuwania t³a
    public float backgroundHeight; // Wysokoœæ t³a (ustaw na wysokoœæ sprite'a)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;

        // Gdy t³o przesunie siê w górê o wysokoœæ jednego obrazka, resetujemy pozycjê
        if (transform.position.y >= startPosition.y + backgroundHeight)
        {
            transform.position -= new Vector3(0, backgroundHeight * 2, 0);
        }
    }
}
