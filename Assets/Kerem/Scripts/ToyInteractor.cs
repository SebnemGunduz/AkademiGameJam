using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyInteractor : MonoBehaviour
{
    [SerializeField] Button repairButton;
    [SerializeField] GameObject panel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dog"))
        {
            repairButton.gameObject.SetActive(true);
        }
        else
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dog"))
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
    }

}
