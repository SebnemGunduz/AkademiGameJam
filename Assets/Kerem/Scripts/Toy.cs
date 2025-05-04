using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    public GameObject repairPanel;
    private bool isFixed = false;

    public void OpenRepairPanel()
    {
        if (!isFixed)
        {
            repairPanel.SetActive(true);

            // DropHandler'e bu oyuncağı bildir
            DropHandler handler = repairPanel.GetComponent<DropHandler>();
            if (handler != null)
            {
                handler.SetCurrentToy(this.gameObject);
            }
        }
    }

    public void MarkAsFixed()
    {
        isFixed = true;
        gameObject.SetActive(false); // Oyuncağı sahneden kaldır
        Part4Manager.Instance.IncreaseScore();
    }

    public bool IsFixed()
    {
        return isFixed;
    }

    public void SetFixed(bool value)
    {
        this.isFixed = value;
    }
}
