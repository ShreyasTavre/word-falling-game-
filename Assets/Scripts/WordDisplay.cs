using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fallSpeed = 1f;

    [SerializeField] private float yPositionToGameOver = -5f;

    public void SetWord(string word)
    {
        text.text = word;
    }

    public void UpdateText(string newText)
    {
        text.text = newText;
    }

    private void Update()
    {
        // Move the word down
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);

        // If a word falls off the bottom of the screen, trigger Game Over
        if (transform.position.y < yPositionToGameOver)
        {
            if (GameOverManager.Instance != null)
            {
                GameOverManager.Instance.TriggerGameOver();
            }
            // Destroy this word to prevent it from triggering game over repeatedly
            Destroy(gameObject);
        }
    }
}