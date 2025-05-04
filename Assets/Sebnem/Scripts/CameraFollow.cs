using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset;    // Kameran�n hedefe olan uzakl��� (X, Y, Z)
    public float smoothSpeed = 0.125f;  // Kameran�n hareket h�z�
    public Vector2 minBounds;  // Kamera i�in minimum s�n�rlar (X, Y)
    public Vector2 maxBounds;  // Kamera i�in maksimum s�n�rlar (X, Y)

    void LateUpdate()
    {
        // Kameray� hedefe do�ru hareket ettiriyoruz.
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamera s�n�rlar� kontrol�
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        transform.position = smoothedPosition;
    }
}
