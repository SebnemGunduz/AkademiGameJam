using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueTrigger1 : MonoBehaviour
{
    [System.Serializable]
    public class DialogueData
    {
        public Sprite image;
        [TextArea]
        public string text;
    }

    // Trigger ile açýlan diyaloglar için
    public GameObject dialoguePanel;
    public Image dialogueImage;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    // Butonla açýlan diyaloglar için
    public GameObject buttonDialoguePanel;
    public Image buttonDialogueImage;
    public TextMeshProUGUI buttonDialogueText;
    public Button buttonNextButton;

    public GameObject childBlockedPanel;

    public List<DialogueData> AdultDialogues;
    public List<DialogueData> ButtonDialogues;

    private int currentDialogueIndex = 0;
    private int currentButtonDialogueIndex = 0;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        buttonDialoguePanel.SetActive(false);
        childBlockedPanel.SetActive(false);

        nextButton.onClick.AddListener(ShowNextDialogue);
        buttonNextButton.onClick.AddListener(ShowNextButtonDialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Adult"))
        {
            StartTriggerDialogue();
        }
        else if (collision.CompareTag("Child"))
        {
            childBlockedPanel.SetActive(true);
        }
    }

    // === Trigger ile açýlan diyalog sistemi ===
    private void StartTriggerDialogue()
    {
        currentDialogueIndex = 0;
        dialoguePanel.SetActive(true);
        ShowDialogueStep(currentDialogueIndex);
    }

    private void ShowDialogueStep(int index)
    {
        if (index < AdultDialogues.Count)
        {
            dialogueImage.sprite = AdultDialogues[index].image;
            dialogueText.text = AdultDialogues[index].text;
        }
    }

    private void ShowNextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < AdultDialogues.Count)
        {
            ShowDialogueStep(currentDialogueIndex);
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

    // === Butonla açýlan diyalog sistemi ===
    public void StartDialogueFromButton()
    {
        currentButtonDialogueIndex = 0;
        buttonDialoguePanel.SetActive(true);
        ShowButtonDialogueStep(currentButtonDialogueIndex);
    }

    private void ShowButtonDialogueStep(int index)
    {
        if (index < ButtonDialogues.Count)
        {
            buttonDialogueImage.sprite = ButtonDialogues[index].image;
            buttonDialogueText.text = ButtonDialogues[index].text;
        }
    }

    private void ShowNextButtonDialogue()
    {
        currentButtonDialogueIndex++;

        if (currentButtonDialogueIndex < ButtonDialogues.Count)
        {
            ShowButtonDialogueStep(currentButtonDialogueIndex);
        }
        else
        {
            buttonDialoguePanel.SetActive(false);
        }
    }
}
