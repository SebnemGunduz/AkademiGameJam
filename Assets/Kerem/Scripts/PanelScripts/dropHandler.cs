using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    [SerializeField] TextMeshProUGUI[] infoElements;     // Tool adlarını gösterecek yazılar
    [SerializeField] GameObject panel_;
    [SerializeField] Transform toolSlotContainer;        // ToolSlot'ları barındıran parent
    [SerializeField] GameObject[] allToolPrefabs;        // Bütün alet prefabları (ToolData'daki ile eşleşecek)

    private Toy currentToy;
    private Stack<GameObject> expectedTools = new Stack<GameObject>();
    private List<GameObject> spawnedTools = new List<GameObject>(); // Oluşan tool instanceları

    public void SetCurrentToy(GameObject toy)
    {
        currentToy = toy.GetComponent<Toy>();
        if (currentToy != null && currentToy.toyData != null)
        {
            SetupRepairSequence(currentToy.toyData);
        }
    }

    private void SetupRepairSequence(ToyData data)
    {
        expectedTools.Clear();

        // 1. Stack'e sırayla pushla (tersten)
        for (int i = data.requiredTools.Length - 1; i >= 0; i--)
        {
            expectedTools.Push(data.requiredTools[i]);
        }

        // 2. Önceki tool instancelarını sil
        foreach (GameObject tool in spawnedTools)
        {
            Destroy(tool);
        }
        spawnedTools.Clear();

        // 3. Rastgele yerleştirme için slotları al ve karıştır
        List<Transform> slots = new List<Transform>();
        foreach (Transform slot in toolSlotContainer)
        {
            slots.Add(slot);
        }
        Shuffle(slots); // Rastgeleleştir

        // 4. Araçları slotlara yerleştir
        for (int i = 0; i < data.requiredTools.Length && i < slots.Count; i++)
        {
            GameObject toolPrefab = data.requiredTools[i];
            GameObject toolInstance = Instantiate(toolPrefab, slots[i]);
            toolInstance.transform.localPosition = Vector3.zero; // Slot ortasına koy
            spawnedTools.Add(toolInstance);
        }

        // 5. Info yazılarını güncelle
        for (int i = 0; i < infoElements.Length; i++)
        {
            if (i < data.requiredTools.Length)
            {
                infoElements[i].text = data.requiredTools[i].tag;
            }
            else
            {
                infoElements[i].text = "";
            }
        }

        // Oyuncak tamirli değilse sıfırla
        currentToy.SetFixed(false);
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

            droppedObj.SetActive(false);
            if (expectedTools.Count == 0)
            {
                Debug.Log("Tamir tamamlandı!");
                currentToy.SetFixed(true);
                currentToy.MarkAsFixed();
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
        if (!currentToy.IsFixed())
        {
            ResetTools();
        }

        panel_.SetActive(false);
    }

    public void ResetTools()
    {
        if (currentToy != null && currentToy.toyData != null)
        {
            SetupRepairSequence(currentToy.toyData); // Baştan yükle
        }
    }

    // Basit bir shuffle algoritması
    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }

}

