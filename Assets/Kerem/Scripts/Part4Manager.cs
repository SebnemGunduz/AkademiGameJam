using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Part4Manager : MonoBehaviour
{
    public static Part4Manager Instance;
    private int fixedToyCount = 0;
    [SerializeField] TextMeshProUGUI scoreText; // UI'da skoru göstermek için
    [SerializeField] int totalToyCount;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        UpdateScoreText();
    }

    public void ToyFixed()
    {
        fixedToyCount++;
        UpdateScoreText();

        if (fixedToyCount >= totalToyCount)
        {
            Debug.Log("Tüm oyuncaklar tamir edildi! Bölüm geçiliyor...");
            LoadNextScene();
        }
    }

    public void UpdateScoreText()
    {
        if(scoreText != null)
        {
            scoreText.text = $"Tamir Edilen Oyuncak: {fixedToyCount}/{totalToyCount}";
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
