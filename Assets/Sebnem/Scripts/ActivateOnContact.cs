using UnityEngine;

public class ActivateOnContact1 : MonoBehaviour
{
    public GameObject interactionPanel;  // Inspector�dan atanacak panel

    private void Start()
    {
        gameObject.SetActive(false); // Nesne sahne ba��nda kapal�
        Invoke(nameof(ActivateObject), 9f); // 9 saniye sonra aktifle�ecek
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
            gameObject.SetActive(false); // Temas sonras� objeyi yok et (deaktif)
        }
    }

    // Paneli kapatmak i�in butonla kullan�lacak fonksiyon
    public void ClosePanel()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }
    }
}
