using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // A chave do Singleton

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    private int score = 0;
    public bool isGameOver = false;

    void Awake()
    {
        // Configuração do Singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        UpdateScoreUI();
    }

    public void AddScore()
    {
        if (isGameOver) return;

        score++;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Verifica se o texto ainda existe antes de atualizar
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo...");
    }
}