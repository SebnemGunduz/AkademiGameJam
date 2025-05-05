using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyInteractor : MonoBehaviour
{
    [SerializeField] Button repairButton;
    [SerializeField] GameObject panel;
    private Toy toy;
    private void Awake()
    {
        toy = GetComponent<Toy>();
    }

    public Button GetRepairButton()
    {
        return repairButton;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!toy.IsFixed())
        {
            if (collision.gameObject.CompareTag("Adult"))
            {
                repairButton.gameObject.SetActive(true);
            }
            else
            {

            }
        }
        else
        {
            repairButton.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Adult"))
        {
            repairButton.gameObject.SetActive(false);
        }
        else
        {

        }
    }

    public void openFixPanel()
    {
        Debug.Log("Buton Çalıştı!");
        panel.SetActive(true);

        DropHandler handler = panel.transform.Find("dropZone").GetComponent<DropHandler>();

        if (handler != null)
        {
            handler.SetCurrentToy(this.gameObject); // BU SATIR EKLENDİ!
            handler.ResetTools(); // Eğer işlem sıfırlamak istiyorsan
        }
    }

}
