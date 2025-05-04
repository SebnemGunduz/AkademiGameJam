using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject[] allTools; // alet prefabları
    [SerializeField] TextMeshProUGUI[] infoElements; // ipuçları için yazılar
    [SerializeField] GameObject panel_;

    private GameObject currentToy; // Bağlı oyuncak
    private Stack<GameObject> expectedTools;
    private void Awake()
    {
        expectedTools = new Stack<GameObject>();

        // Son kullanılması gereken en üstte olacak şekilde sırala
        expectedTools.Push(allTools[3]);
        expectedTools.Push(allTools[2]);
        expectedTools.Push(allTools[1]);
        expectedTools.Push(allTools[0]);

        // infoElements dizisini doğrudan kullan
        for (int i = 0; i < infoElements.Length && i < allTools.Length; i++)
        {
            if (infoElements[i] != null && allTools[i] != null)
            {
                infoElements[i].text = allTools[i].tag;
            }
        }

    }
    public void SetCurrentToy(GameObject toy)
    {
        currentToy = toy;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (expectedTools.Count == 0) return;
        
        GameObject droppedObj = eventData.pointerDrag;
        GameObject expectedTool = expectedTools.Peek();

        if (droppedObj != null && droppedObj.tag == expectedTool.tag)
        {
            expectedTools.Pop();
            Debug.Log("Doğru alet kullanıldı!");
            if (expectedTools.Count == 0)
            {
                Debug.Log("Tamir tamamlandı!");
                if (currentToy != null)
                {
                    currentToy.GetComponent<Toy>().MarkAsFixed(); // Oyuncağı bildir
                }
                QuitPanel();
            }
        }
        else
        {
            Debug.Log("Yanlış alet!");
        }
    }

    public void QuitPanel()
    {
        bool fix = currentToy.GetComponent<Toy>().IsFixed();
        if (!fix)
        {
            ResetTools(); // Stack’i eski haline getir
        }
        panel_.SetActive(false);
    }

    public void ResetTools()
    {
        expectedTools.Clear();
        expectedTools.Push(allTools[3]);
        expectedTools.Push(allTools[2]);
        expectedTools.Push(allTools[1]);
        expectedTools.Push(allTools[0]);

        for (int i = 0; i < infoElements.Length && i < allTools.Length; i++)
        {
            infoElements[i].text = allTools[i].tag;
        }

        currentToy.GetComponent <Toy>().SetFixed(false);
    }

}

