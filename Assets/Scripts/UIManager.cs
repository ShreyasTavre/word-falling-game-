using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindAnyObjectByType<UIManager>();
            return _instance;
        }
    }

    public enum GameState { Playing, Paused, GameOver, GameFinished }
    public GameState currentState;

    [Header("UI Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameFinishedPanel;

    [Header("Game HUD")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timerText;

    [Header("Typeable UI Options")]
    [SerializeField] private List<TypeableOption> pauseOptions;
    [SerializeField] private List<TypeableOption> gameOverOptions;
    [SerializeField] private List<TypeableOption> gameFinishedOptions;
    private TypeableOption activeOption;

    private int score = 0;
    private int lives = 3;

    [Header("Timer Settings")]
    public float gameTimeInSeconds = 60f;
    private float timeRemaining;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScore(0);
        UpdateLives(3);
        timeRemaining = gameTimeInSeconds;
        ResumeGame(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
        
        if (currentState == GameState.Playing)
        {
            HandleTimer();
        }
    }
    
    private void HandleTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
            }
        }
        else
        {
            timeRemaining = 0;
            if (timerText != null)
            {
                timerText.text = "0";
            }
            TriggerGameFinished();
        }
    }
    
    public void AddScore(int points)
    {
        UpdateScore(score + points);
    }

    private void UpdateScore(int newScore)
    {
        score = newScore;
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score}";
        }
    }

    public void LoseLife()
    {
        UpdateLives(lives - 1);
        // This check "uses" the lives variable, fixing the warning.
        if (lives <= 0)
        {
            if (GameOverManager.Instance != null)
            {
                GameOverManager.Instance.TriggerGameOver();
            }
        }
    }

    private void UpdateLives(int newLives)
    {
        lives = newLives;
        if (livesText != null)
        {
            livesText.text = $"LIVES: {lives}";
        }
    }
    
    public void TypeUIOption(char letter)
    {
        List<TypeableOption> currentOptions = GetCurrentOptions();
        if (currentOptions == null) return;

        if (activeOption != null)
        {
            if (activeOption.GetNextLetter() == letter)
            {
                activeOption.TypeLetter();
            }
        }
        else
        {
            activeOption = currentOptions.FirstOrDefault(option => option.GetNextLetter() == letter);
            if (activeOption != null)
            {
                activeOption.TypeLetter();
            }
        }

        if (activeOption != null && activeOption.IsTyped())
        {
            activeOption.InvokeTypedEvent();
            activeOption = null;
        }
    }

    private List<TypeableOption> GetCurrentOptions()
    {
        switch (currentState)
        {
            case GameState.Paused:
                return pauseOptions;
            case GameState.GameOver:
                return gameOverOptions;
            case GameState.GameFinished:
                return gameFinishedOptions;
            default:
                return null;
        }
    }

    public void PauseGame()
    {
        currentState = GameState.Paused;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        ResetAllOptions();
    }

    public void ResumeGame()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        if (gameFinishedPanel != null) gameFinishedPanel.SetActive(false);
        ResetAllOptions();
    }
    
    public void ShowGameOver()
    {
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        ResetAllOptions();
    }
    
    public void TriggerGameFinished()
    {
        Debug.Log("--- GAME FINISHED (TIMER ENDED) ---");
        currentState = GameState.GameFinished;
        Time.timeScale = 0f;
        if (gameFinishedPanel != null)
        {
            gameFinishedPanel.SetActive(true);
            TextMeshProUGUI finalScoreText = gameFinishedPanel.GetComponentInChildren<TextMeshProUGUI>();
            if (finalScoreText != null)
            {
                finalScoreText.text = $"FINAL SCORE:\n{score}";
            }
        }
        ResetAllOptions();
    }

    private void ResetAllOptions()
    {
        activeOption = null;
        if (pauseOptions != null) { foreach (var option in pauseOptions) { if (option != null) option.Reset(); } }
        if (gameOverOptions != null) { foreach (var option in gameOverOptions) { if (option != null) option.Reset(); } }
        if (gameFinishedOptions != null) { foreach (var option in gameFinishedOptions) { if (option != null) option.Reset(); } }
    }
}