using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ColorMatchGame : MonoBehaviour
{
    public List<Color> colors; // Kullanýlabilecek renkler
    public Image colorIndicator; // Renk belirteci
    public GameObject circlePrefab; // Hareket eden yuvarlak prefabý
    public RectTransform[] spawnPoints; // Spawn noktalarý
    public int maxCircles = 5; // Maksimum ayný anda aktif olabilecek yuvarlak sayýsý
    public float spawnInterval = 2.0f; // Objelerin oluþturulma aralýðý

    public TextMeshProUGUI scoreText; // Puan gösterimi
    public Slider timerSlider; // Zamanlayýcý sliderý
    public Button startButton; // Oyunu baþlatma tuþu
    public Button quitButton; // Oyunu bitirme tuþu
    public GameObject startPanel; // Oyunun baþlangýç paneli
    public GameObject gamePanel; // Oyunun oyun alaný paneli

    public GameObject finishPanel; // Oyun sonu paneli
    public TextMeshProUGUI finalScoreText; // Oyun sonu skoru
    public Button restartButton; // Tekrar baþlatma butonu
    public int againScore;
    private bool hasRestarted = false; // RestartGame'in sadece bir kez çalýþmasýný saðlamak için

    private Color targetColor; // Þu anki renk belirteci
    private int score = 0; // Puan
    private float timer = 30.0f; // Oyun süresi
    private bool isGameActive = false; // Oyun aktiflik durumu

    private void Start()
    {
        hasRestarted = false;
        timerSlider.maxValue = timer;
        timerSlider.value = timer;
        scoreText.text = "Score: " + score;

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(RestartGame);
        startPanel.SetActive(true);
        finishPanel.SetActive(false);
    }

    private void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            timerSlider.value = timer;

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    private void StartGame()
    {
        isGameActive = true;
        startPanel.SetActive(false);
        if (!hasRestarted)
        {
            score = 0; // Puaný sýfýrla
        }
        else
        {
            score = againScore;
        }
        timer = 30.0f;
        scoreText.text = "Score: " + score;
        timerSlider.value = timer;

        UpdateColorIndicator();
        InvokeRepeating(nameof(SpawnCircle), 0.5f, spawnInterval);
    }

    private void EndGame()
    {
        finalScoreText.text = score.ToString();
        finishPanel.SetActive(true);
        isGameActive = false;
        CancelInvoke(nameof(SpawnCircle));

        if (!hasRestarted)
        {
            restartButton.gameObject.SetActive(true);
        }
        else
        {
            restartButton.gameObject.SetActive(false);
        }
    }

    private void QuitGame()
    {
        if (isGameActive)
        {
            EndGame();
        }

        gamePanel.SetActive(false);
    }

    public void RestartGame()
    {
        if (!hasRestarted)
        { // Eðer daha önce çaðrýldýysa, iþlemi sonlandýr

            hasRestarted = true; // Ýlk çaðrýldýðýnda deðiþkeni true yap

            againScore = score;
            startPanel.SetActive(true);
            finishPanel.SetActive(false);
            gamePanel.SetActive(true);

            StartGame();
        }
    }

    private void UpdateColorIndicator()
    {
        targetColor = colors[Random.Range(0, colors.Count)];
        colorIndicator.color = targetColor;
    }

    private void SpawnCircle()
    {
        if (!isGameActive) return;

        if (GameObject.FindGameObjectsWithTag("Circle").Length >= maxCircles) return;

        // Rastgele bir spawn noktasý seç
        RectTransform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject circle = Instantiate(circlePrefab, spawnPoint.position, Quaternion.identity);

        // Canvas altýnda child olarak ayarla
        circle.transform.SetParent(colorIndicator.canvas.transform, false);
        circle.GetComponent<RectTransform>().anchoredPosition = spawnPoint.anchoredPosition;

        // Rengini ata
        Color circleColor = colors[Random.Range(0, colors.Count)];
        circle.GetComponent<Image>().color = circleColor;

        // Button için týklama olayý ekle
        Button circleButton = circle.GetComponent<Button>();
        circleButton.onClick.AddListener(() => OnCircleClicked(circle, circleColor));

        // Yuvarlak hareketini baþlat
        var movement = circle.AddComponent<CircleMovement>();
        movement.Initialize();
    }

    private void OnCircleClicked(GameObject circle, Color circleColor)
    {
        if (!isGameActive) return;

        if (circleColor == targetColor)
        {
            score += 10;
            UpdateColorIndicator();
        }
        else
        {
            score -= 5;
            if (score < 0) score = 0;
        }

        scoreText.text = "Score: " + score;
        Destroy(circle);
    }
}
