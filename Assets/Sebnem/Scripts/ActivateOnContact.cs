using UnityEngine;

public class ActivateOnContact1 : MonoBehaviour
{
    public GameObject interactionPanel;  // Inspector’dan atanacak panel

    private void Start()
    {
        gameObject.SetActive(false); // Nesne sahne baþýnda kapalý
        Invoke(nameof(ActivateObject), 9f); // 9 saniye sonra aktifleþecek
    }

    private void ActivateObject()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Adult") && interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            gameObject.SetActive(false); // Temas sonrasý objeyi yok et (deaktif)
        }
    }

    // Paneli kapatmak için butonla kullanýlacak fonksiyon
    public void ClosePanel()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }
    }
}
