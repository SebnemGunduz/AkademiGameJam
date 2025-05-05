using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public class DialogueData
    {
        public Sprite characterImage;
        [TextArea]
        public string dialogueText;
    }

    public GameObject dialoguePanel;
    public Image charImage;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    public List<DialogueData> AdultDialogues;
    public List<DialogueData> ChildDialogues;
    public string nextSceneName;
    public CharacterSwitcher characterSwitcher;
    public bool nextSceneBool = false;

    private bool goToNextSceneAfterDialogue = false;
    private List<DialogueData> currentDialogueList;
    private int currentDialogueIndex;
    private bool adultDialoguePlayed = false;
    private bool childDialoguePlayed = false;

    private void Start()
    {
        nextButton.onClick.AddListener(NextDialogue);
        dialoguePanel.SetActive(false); // baþlangýçta gizli olmalý
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Adult") && !adultDialoguePlayed)
        {
            currentDialogueList = AdultDialogues;
            adultDialoguePlayed = true;
            goToNextSceneAfterDialogue = false;
            StartDialogue();
        }
        else if (collision.CompareTag("Child") && !childDialoguePlayed)
        {
            currentDialogueList = ChildDialogues;
            childDialoguePlayed = true;
            goToNextSceneAfterDialogue = true;
            StartDialogue();
        }
    }

    private void StartDialogue()
    {
        characterSwitcher.isCanWalk = false;
        currentDialogueIndex = 0;
        dialoguePanel.SetActive(true);
        LoadCurrentDialogue();
    }

    private void LoadCurrentDialogue()
    {
        if (currentDialogueList != null && currentDialogueIndex < currentDialogueList.Count)
        {
            var data = currentDialogueList[currentDialogueIndex];
            charImage.sprite = data.characterImage;
            dialogueText.text = data.dialogueText;
        }
    }

    private void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueList != null && currentDialogueIndex < currentDialogueList.Count)
        {
            LoadCurrentDialogue();
        }
        else
        {
            // Diyalog bitti
            dialoguePanel.SetActive(false);
            characterSwitcher.isCanWalk = true;

            if (goToNextSceneAfterDialogue && nextSceneBool)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
