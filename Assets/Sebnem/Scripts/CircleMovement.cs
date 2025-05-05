using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    private float verticalSpeed;   // Yukar� do�ru hareket h�z�
    private float horizontalSpeed; // Yatay hareket h�z�
    private int horizontalDirection; // -1: sola, 1: sa�a, 0: sabit

    public void Initialize()
    {
        verticalSpeed = Random.Range(400, 500); // Yukar� do�ru hareket h�z�
        horizontalSpeed = Random.Range(20, 50); // Yatay hareket h�z�
        horizontalDirection = Random.Range(-1, 2); // -1 (sola), 0 (sabit), 1 (sa�a)
    }

    private void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Yukar� hareket
        Vector2 position = rectTransform.anchoredPosition;
        position.y += verticalSpeed * Time.deltaTime;

        // Yatay hareket (sadece sa�a, sola veya sabit)
        position.x += horizontalDirection * horizontalSpeed * Time.deltaTime;

        rectTransform.anchoredPosition = position;

        // Ekrandan ��kt�ysa objeyi yok et
        if (rectTransform.anchoredPosition.y > Screen.height - 250 ||
            rectTransform.anchoredPosition.x < -Screen.width - 100 ||
            rectTransform.anchoredPosition.x > Screen.width + 100)
        {
            Destroy(gameObject);
        }
    }
}
