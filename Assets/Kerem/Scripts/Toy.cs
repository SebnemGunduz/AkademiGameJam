using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour
{
    public GameObject repairPanel;
    private bool isFixed = false;
    public ToyData toyData;
    public ToyData GetData() => toyData;
    private ToyInteractor interactor;

    private void Awake()
    {
        interactor = GetComponent<ToyInteractor>();
    }
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
        //gameObject.SetActive(false); // Oyuncağı sahneden kaldır
        interactor.GetRepairButton().gameObject.SetActive(false);
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && toyData.toyFixedImage != null)
        {
            sr.sprite = toyData.toyFixedImage;
        }
        Debug.Log("Tamir Edildi!");
        // Bölüm yöneticisine bildir
        Part4Manager.Instance?.ToyFixed();
        //art4Manager.Instance.IncreaseScore();
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
