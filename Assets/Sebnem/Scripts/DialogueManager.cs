using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;
    public Button nextButton;

    private List<DialogueData> currentDialogues;
    private int currentIndex;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(NextDialogue);
    }

    public void StartDialogue(DialogueTrigger trigger, string characterTag)
    {
        // Trigger’dan doðru listeyi al
        if (characterTag == "Adult")
            currentDialogues = trigger.AdultDialogues;
        else if (characterTag == "Child")
            currentDialogues = trigger.ChildDialogues;

        if (currentDialogues == null || currentDialogues.Count == 0)
            return;

        currentIndex = -1;
        dialoguePanel.SetActive(true);
        ShowDialogue(currentIndex);
    }

    private void ShowDialogue(int index)
    {
        dialogueText.text = currentDialogues[index].dialogueText;
        characterImage.sprite = currentDialogues[index].characterImage;
    }

    private void NextDialogue()
    {
        currentIndex++;

        if (currentIndex < currentDialogues.Count)
        {
            ShowDialogue(currentIndex);
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }

}