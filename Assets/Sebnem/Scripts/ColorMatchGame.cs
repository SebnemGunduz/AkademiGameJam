using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ColorMatchGame : MonoBehaviour
{
    public List<Color> colors; // Kullan�labilecek renkler
    public Image colorIndicator; // Renk belirteci
    public GameObject circlePrefab; // Hareket eden yuvarlak prefab�
    public RectTransform[] spawnPoints; // Spawn noktalar�
    public int maxCircles = 5; // Maksimum ayn� anda aktif olabilecek yuvarlak say�s�
    public float spawnInterval = 2.0f; // Objelerin olu�turulma aral���

    public TextMeshProUGUI scoreText; // Puan g�sterimi
    public Slider timerSlider; // Zamanlay�c� slider�
    public Button startButton; // Oyunu ba�latma tu�u
    public Button quitButton; // Oyunu bitirme tu�u
    public GameObject startPanel; // Oyunun ba�lang�� paneli
    public GameObject gamePanel; // Oyunun oyun alan� paneli

    public GameObject finishPanel; // Oyun sonu paneli
    public TextMeshProUGUI finalScoreText; // Oyun sonu skoru
    public Button restartButton; // Tekrar ba�latma butonu
    public int againScore;
    private bool hasRestarted = false; // RestartGame'in sadece bir kez �al��mas�n� sa�lamak i�in

    private Color targetColor; // �u anki renk belirteci
    private int score = 0; // Puan
    private float timer = 30.0f; // Oyun s�resi
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
            score = 0; // Puan� s�f�rla
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
        { // E�er daha �nce �a�r�ld�ysa, i�lemi sonland�r

            hasRestarted = true; // �lk �a�r�ld���nda de�i�keni true yap

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

        // Rastgele bir spawn noktas� se�
        RectTransform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject circle = Instantiate(circlePrefab, spawnPoint.position, Quaternion.identity);

        // Canvas alt�nda child olarak ayarla
        circle.transform.SetParent(colorIndicator.canvas.transform, false);
        circle.GetComponent<RectTransform>().anchoredPosition = spawnPoint.anchoredPosition;

        // Rengini ata
        Color circleColor = colors[Random.Range(0, colors.Count)];
        circle.GetComponent<Image>().color = circleColor;

        // Button i�in t�klama olay� ekle
        Button circleButton = circle.GetComponent<Button>();
        circleButton.onClick.AddListener(() => OnCircleClicked(circle, circleColor));

        // Yuvarlak hareketini ba�lat
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
