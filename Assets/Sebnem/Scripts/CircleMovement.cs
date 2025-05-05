using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    private float verticalSpeed;   // Yukarý doðru hareket hýzý
    private float horizontalSpeed; // Yatay hareket hýzý
    private int horizontalDirection; // -1: sola, 1: saða, 0: sabit

    public void Initialize()
    {
        verticalSpeed = Random.Range(400, 500); // Yukarý doðru hareket hýzý
        horizontalSpeed = Random.Range(20, 50); // Yatay hareket hýzý
        horizontalDirection = Random.Range(-1, 2); // -1 (sola), 0 (sabit), 1 (saða)
    }

    private void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Yukarý hareket
        Vector2 position = rectTransform.anchoredPosition;
        position.y += verticalSpeed * Time.deltaTime;

        // Yatay hareket (sadece saða, sola veya sabit)
        position.x += horizontalDirection * horizontalSpeed * Time.deltaTime;

        rectTransform.anchoredPosition = position;

        // Ekrandan çýktýysa objeyi yok et
        if (rectTransform.anchoredPosition.y > Screen.height - 250 ||
            rectTransform.anchoredPosition.x < -Screen.width - 100 ||
            rectTransform.anchoredPosition.x > Screen.width + 100)
        {
            Destroy(gameObject);
        }
    }
}
