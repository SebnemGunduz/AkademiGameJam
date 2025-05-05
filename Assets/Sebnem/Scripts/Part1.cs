using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Part1 : MonoBehaviour
{
    [Header("UI Referanslar�")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public Button nextButton;

    [Header("Diyaloglar ve G�rseller")]
    [TextArea(2, 5)]
    public List<string> dialogues;
    public List<Sprite> images;

    public CharacterSwitcher characterController;
    private int currentIndex = 0;

    void Start()
    {
        // Her �ey haz�r m� kontrol et
        if (dialoguePanel == null || dialogueText == null || characterImage == null || nextButton == null)
        {
            Debug.LogError("DialogueManager: UI elemanlar� eksik!");
            return;
        }

        // Paneli a� ve oyunu durdur
        dialoguePanel.SetActive(true);

        // �lk diyalo�u g�ster
        if (dialogues.Count > 0)  // Diyaloglar�n olup olmad���n� kontrol et
        {
            ShowDialogue(0); // Ba�lang�� diyalo�unu g�ster
        }

        // Next butonuna fonksiyon ba�la
        nextButton.onClick.AddListener(NextDialogue);
    }

    void ShowDialogue(int index)
    {
        characterController.isCanWalk = false;
        // E�er index ge�erli de�ilse, diyalo�u g�stermiyoruz.
        if (index >= 0 && index < dialogues.Count)
        {
            dialogueText.text = dialogues[index];

            // G�rsel varsa, onu da g�ster
            if (index < images.Count && images[index] != null)
            {
                characterImage.sprite = images[index];
                characterImage.enabled = true;
            }
            else
            {
                characterImage.enabled = false;
            }
        }
        else
        {
            // E�er index ge�ersizse, hatay� logla
            Debug.LogError("Ge�ersiz indeks: " + index);
        }
    }

    void NextDialogue()
    {
        currentIndex++;

        // E�er ge�erli diyalo�un sonuna gelmediysek, yeni diyalo�u g�ster
        if (currentIndex < dialogues.Count)
        {
            ShowDialogue(currentIndex);
        }
        else
        {
            EndDialogue(); // E�er diyalo�un sonuna gelindiyse, diyalo�u bitir
        }
    }

    void EndDialogue()
    {
        characterController.isCanWalk = true;
        dialoguePanel.SetActive(false); // Paneli kapat
    }
}
