using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogStarter : MonoBehaviour
{
    [SerializeField] GameObject dialogPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Adult"))
        {
            dialogPanel.SetActive(true);
        }
    }
}
