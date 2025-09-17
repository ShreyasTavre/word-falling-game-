using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private static GameOverManager _instance;
    public static GameOverManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindAnyObjectByType<GameOverManager>();
            return _instance;
        }
    }

    public WordTimer wordTimer;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    public void TriggerGameOver()
    {
        Debug.Log("--- GAME OVER TRIGGERED! ---");
    
        if (wordTimer != null) wordTimer.enabled = false;
        
        // --- ADDED LINE ---
        // Also disable the main timer in UIManager
        if (UIManager.Instance != null)
        {
            UIManager.Instance.enabled = false;
        }

        GameObject[] wordsOnScreen = GameObject.FindGameObjectsWithTag("Word");
        foreach (GameObject word in wordsOnScreen)
        {
            Destroy(word);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOver();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game... (Note: This only works in a built application, not the editor)");
        Application.Quit();
    }
}