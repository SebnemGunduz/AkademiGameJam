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

    public GameObject startPanel;              // Ba�lang�� ekran� paneli
    public Button playButton;                  // Ba�lang�� ekran� butonu

    public GameObject dialoguePanel;           // T�m diyalog aray�z�n� kapsayan panel
    public Image displayImage;                 // Sprite'� g�sterecek Image
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

        // Ba�lang��ta sadece startPanel a��k, diyalog kapal�
        startPanel.SetActive(true);
        dialoguePanel.SetActive(false);
    }

    private void StartDialogue()
    {
        startPanel.SetActive(false);       // Ba�lang�� ekran� kapan�r
        dialoguePanel.SetActive(true);     // Diyalog aray�z� a��l�r

        currentSpriteIndex = 0;
        currentDialogueIndex = 0;

        LoadCurrentDialogue();
    }

    private void LoadCurrentDialogue()
    {
        if (currentSpriteIndex >= Images.Count)
        {
            // Diyaloglar bitti, istersen sahne ge�i�i yap�labilir
            Debug.Log("T�m diyaloglar tamamland�!");
            dialoguePanel.SetActive(false);
            SceneManager.LoadScene(nextSceneName);
            return;
        }

        var currentImageDialogue = Images[currentSpriteIndex];

        if (currentDialogueIndex < currentImageDialogue.Chars.Count)
        {
            // Mevcut metni ve karakter g�rselini y�kle
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
            // Bir sonraki ImageDialogue'a ge�
            currentDialogueIndex = 0;
            currentSpriteIndex++;

            if (currentSpriteIndex < Images.Count)
            {
                // Yeni bir displayImage y�klenir
                displayImage.sprite = Images[currentSpriteIndex].image;
            }
        }

        LoadCurrentDialogue();
    }

}
