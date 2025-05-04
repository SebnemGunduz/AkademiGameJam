using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject[] allTools; // alet prefabları
    private Stack<GameObject> expectedTools;
    [SerializeField] TextMeshProUGUI[] infoElements; // ipuçları için yazılar

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

    public void OnDrop(PointerEventData eventData)
    {
        if (expectedTools.Count == 0) return;

        GameObject droppedObj = eventData.pointerDrag;
        GameObject expectedTool = expectedTools.Peek();

        if (droppedObj != null && droppedObj.tag == expectedTool.tag)
        {
            expectedTools.Pop();
            Debug.Log("Doğru alet kullanıldı!");
            // örn. droppedObj.SetActive(false);
        }
        else
        {
            Debug.Log("Yanlış alet!");
        }
    }
}

