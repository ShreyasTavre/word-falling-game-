using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fallSpeed = 1f;

    [SerializeField] private float yPositionToGameOver = -5f;
    private bool hasFallen = false;

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
        transform.Translate(0f, -fallSpeed * Time.deltaTime, 0f);

        if (transform.position.y < yPositionToGameOver && !hasFallen)
        {
            hasFallen = true; 
            
            if (UIManager.Instance != null)
            {
                UIManager.Instance.LoseLife();
            }
            
            Destroy(gameObject);
        }
    }
}