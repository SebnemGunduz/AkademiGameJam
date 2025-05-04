using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhoneController : MonoBehaviour
{
    public GameObject phoneButton;
    public GameObject callPanel;
    public GameObject dialogPanel;
    public GameObject characterObject; // Inspector'dan atayacaðýn karakter

    public TextMeshProUGUI numberDisplay;
    public Image dialogImage;
    public TextMeshProUGUI dialogText;
    public Button nextButton;

    private string dialedNumber = "";
    private const int maxNumberLength = 5;
    private string correctNumber = "76592";

    [System.Serializable]
    public class DialogStep
    {
        public string text;
        public Sprite image;
    }

    public DialogStep[] dialogSteps;
    private int currentStepIndex = 0;

    private void Update()
    {
        GameObject catObject = GameObject.FindWithTag("Adult");

        if (catObject != null && !phoneButton.activeSelf)
        {
            phoneButton.SetActive(true);
        }
        else if (catObject == null && phoneButton.activeSelf)
        {
            phoneButton.SetActive(false);
        }
    }

    private void Start()
    {
        
        callPanel.SetActive(false);
        dialogPanel.SetActive(false);

        nextButton.onClick.AddListener(ShowNextDialogStep);
    }

    

    public void OpenCallPanel()
    {
        
        callPanel.SetActive(true);
        numberDisplay.text = "";
        dialedNumber = "";
    }

    public void OnDialButtonPressed(string digit)
    {
        if (dialedNumber.Length < maxNumberLength)
        {
            dialedNumber += digit;
            numberDisplay.text = dialedNumber;
        }
    }

    public void OnDeleteButtonPressed()
    {
        if (dialedNumber.Length > 0)
        {
            dialedNumber = dialedNumber.Substring(0, dialedNumber.Length - 1);
            numberDisplay.text = dialedNumber;
        }
    }

    public void OnCallButtonPressed()
    {
        if (dialedNumber == correctNumber)
        {
            callPanel.SetActive(false);
            dialogPanel.SetActive(true);
            currentStepIndex = 0;
            ShowDialogStep(currentStepIndex);
        }
    }

    private void ShowDialogStep(int index)
    {
        dialogText.text = dialogSteps[index].text;
        dialogImage.sprite = dialogSteps[index].image;
    }

    private void ShowNextDialogStep()
    {
        currentStepIndex++;

        if (currentStepIndex < dialogSteps.Length)
        {
            ShowDialogStep(currentStepIndex);
        }
        else
        {
            dialogPanel.SetActive(false); // Diyaloglar bitti
        }
    }

    // Arama panelinden çýkýþ fonksiyonu
    public void ExitCallPanel()
    {
        callPanel.SetActive(false);
        
    }

    // Dial buttons
    public void OnButton0Pressed() => OnDialButtonPressed("0");
    public void OnButton1Pressed() => OnDialButtonPressed("1");
    public void OnButton2Pressed() => OnDialButtonPressed("2");
    public void OnButton3Pressed() => OnDialButtonPressed("3");
    public void OnButton4Pressed() => OnDialButtonPressed("4");
    public void OnButton5Pressed() => OnDialButtonPressed("5");
    public void OnButton6Pressed() => OnDialButtonPressed("6");
    public void OnButton7Pressed() => OnDialButtonPressed("7");
    public void OnButton8Pressed() => OnDialButtonPressed("8");
    public void OnButton9Pressed() => OnDialButtonPressed("9");
}
