using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Part1 : MonoBehaviour
{
    [Header("UI Referanslarý")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public Button nextButton;

    [Header("Diyaloglar ve Görseller")]
    [TextArea(2, 5)]
    public List<string> dialogues;
    public List<Sprite> images;

    public CharacterSwitcher characterController;
    private int currentIndex = 0;

    void Start()
    {
        // Her þey hazýr mý kontrol et
        if (dialoguePanel == null || dialogueText == null || characterImage == null || nextButton == null)
        {
            Debug.LogError("DialogueManager: UI elemanlarý eksik!");
            return;
        }

        // Paneli aç ve oyunu durdur
        dialoguePanel.SetActive(true);

        // Ýlk diyaloðu göster
        if (dialogues.Count > 0)  // Diyaloglarýn olup olmadýðýný kontrol et
        {
            ShowDialogue(0); // Baþlangýç diyaloðunu göster
        }

        // Next butonuna fonksiyon baðla
        nextButton.onClick.AddListener(NextDialogue);
    }

    void ShowDialogue(int index)
    {
        characterController.isCanWalk = false;
        // Eðer index geçerli deðilse, diyaloðu göstermiyoruz.
        if (index >= 0 && index < dialogues.Count)
        {
            dialogueText.text = dialogues[index];

            // Görsel varsa, onu da göster
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
            // Eðer index geçersizse, hatayý logla
            Debug.LogError("Geçersiz indeks: " + index);
        }
    }

    void NextDialogue()
    {
        currentIndex++;

        // Eðer geçerli diyaloðun sonuna gelmediysek, yeni diyaloðu göster
        if (currentIndex < dialogues.Count)
        {
            ShowDialogue(currentIndex);
        }
        else
        {
            EndDialogue(); // Eðer diyaloðun sonuna gelindiyse, diyaloðu bitir
        }
    }

    void EndDialogue()
    {
        characterController.isCanWalk = true;
        dialoguePanel.SetActive(false); // Paneli kapat
    }
}
