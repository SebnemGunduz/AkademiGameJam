using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset;    // Kameranýn hedefe olan uzaklýðý (X, Y, Z)
    public float smoothSpeed = 0.125f;  // Kameranýn hareket hýzý
    public Vector2 minBounds;  // Kamera için minimum sýnýrlar (X, Y)
    public Vector2 maxBounds;  // Kamera için maksimum sýnýrlar (X, Y)

    void LateUpdate()
    {
        // Kamerayý hedefe doðru hareket ettiriyoruz.
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamera sýnýrlarý kontrolü
        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        transform.position = smoothedPosition;
    }
}
