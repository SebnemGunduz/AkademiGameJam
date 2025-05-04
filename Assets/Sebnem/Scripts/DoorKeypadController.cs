using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorKeypadController : MonoBehaviour
{
    public TextMeshProUGUI codeText; // Kodlarý yazdýracaðýmýz TextMeshPro
    public AudioClip correctPasswordAudio; // Doðru þifre sesi
    public AudioClip wrongPasswordAudio;   // Yanlýþ þifre sesi
    public AudioSource audioSource;

    private string correctPassword = "8358"; // Doðru þifre
    private string enteredCode = ""; // Kullanýcýnýn girdiði þifreyi tutan string

    private void Start()
    {
        codeText.text = "";
        codeText.color = Color.white;
    }

    public void OnDialButtonPressed(string buttonValue)
    {
        if (enteredCode.Length < 8)
        {
            enteredCode += buttonValue;
            codeText.text = enteredCode;
            codeText.color = Color.white;
        }
    }

    public void ClearCode()
    {
        enteredCode = "";
        codeText.text = "";
        codeText.color = Color.white;
    }

    public void CheckPassword()
    {
        if (enteredCode == correctPassword)
        {
            StartCoroutine(PlayCorrectSound());
        }
        else
        {
            audioSource.clip = wrongPasswordAudio;
            audioSource.Play();
            codeText.color = Color.red;
            enteredCode = "";
        }
    }

    private IEnumerator PlayCorrectSound()
    {
        audioSource.clip = correctPasswordAudio;
        audioSource.Play();
        codeText.color = Color.green;
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("Part4");
        Debug.Log("Doðru þifre girildi!");
    }

    public void OnButton1Pressed() => OnDialButtonPressed("1");
    public void OnButton2Pressed() => OnDialButtonPressed("2");
    public void OnButton3Pressed() => OnDialButtonPressed("3");
    public void OnButton4Pressed() => OnDialButtonPressed("4");
    public void OnButton5Pressed() => OnDialButtonPressed("5");
    public void OnButton6Pressed() => OnDialButtonPressed("6");
    public void OnButton7Pressed() => OnDialButtonPressed("7");
    public void OnButton8Pressed() => OnDialButtonPressed("8");
    public void OnButton9Pressed() => OnDialButtonPressed("9");
    public void OnButton0Pressed() => OnDialButtonPressed("0");

    public void OnButtonCPressed() => ClearCode();
    public void OnButtonEPressed() => CheckPassword();
}
