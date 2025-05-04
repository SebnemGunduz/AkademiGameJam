using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Part4Manager : MonoBehaviour
{
    public static Part4Manager Instance;
    public int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Skor: " + score.ToString();
    }
}
