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

    public GameObject gameOverUIPanel;
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

        GameObject[] wordsOnScreen = GameObject.FindGameObjectsWithTag("Word");
        foreach (GameObject word in wordsOnScreen)
        {
            Destroy(word);
        }

        if (gameOverUIPanel != null) gameOverUIPanel.SetActive(true);
        
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}