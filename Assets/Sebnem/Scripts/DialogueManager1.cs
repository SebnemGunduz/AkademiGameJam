using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class DialogueManager1 : MonoBehaviour
{
    [System.Serializable]
    public class CharDialogue
    {
        public Sprite CharImage;
        public string CharText;
    }

    [System.Serializable]
    public class ImageDialogue
    {
        public Sprite image;
        public List<CharDialogue> Chars;
    }

    public GameObject startPanel;              // Baþlangýç ekraný paneli
    public Button playButton;                  // Baþlangýç ekraný butonu

    public GameObject dialoguePanel;           // Tüm diyalog arayüzünü kapsayan panel
    public Image displayImage;                 // Sprite'ý gösterecek Image
    public Image charImage;
    public TextMeshProUGUI dialogueText;                  // Diyalog metni
    public Button nextButton;                  // "Sonraki" butonu
    public List<ImageDialogue> Images;
    public string nextSceneName;

    private int currentSpriteIndex = 0;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        playButton.onClick.AddListener(StartDialogue);
        nextButton.onClick.AddListener(NextDialogue);

        // Baþlangýçta sadece startPanel açýk, diyalog kapalý
        startPanel.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    private void StartDialogue()
    {
        startPanel.SetActive(false);       // Baþlangýç ekraný kapanýr
        dialoguePanel.SetActive(true);     // Diyalog arayüzü açýlýr

        currentSpriteIndex = 0;
        currentDialogueIndex = 0;

        LoadCurrentDialogue();
    }

    private void LoadCurrentDialogue()
    {
        if (currentSpriteIndex >= Images.Count)
        {
            // Diyaloglar bitti, istersen sahne geçiþi yapýlabilir
            Debug.Log("Tüm diyaloglar tamamlandý!");
            dialoguePanel.SetActive(false);
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        var currentImageDialogue = Images[currentSpriteIndex];

        if (currentDialogueIndex < currentImageDialogue.Chars.Count)
        {
            // Mevcut metni ve karakter görselini yükle
            var currentCharDialogue = currentImageDialogue.Chars[currentDialogueIndex];
            charImage.sprite = currentCharDialogue.CharImage;
            dialogueText.text = currentCharDialogue.CharText;
        }
    }

    private void NextDialogue()
    {
        currentDialogueIndex++;

        var currentImageDialogue = Images[currentSpriteIndex];

        if (currentDialogueIndex >= currentImageDialogue.Chars.Count)
        {
            // Bir sonraki ImageDialogue'a geç
            currentDialogueIndex = 0;
            currentSpriteIndex++;

            if (currentSpriteIndex < Images.Count)
            {
                // Yeni bir displayImage yüklenir
                displayImage.sprite = Images[currentSpriteIndex].image;
            }
        }

        LoadCurrentDialogue();
    }

}
