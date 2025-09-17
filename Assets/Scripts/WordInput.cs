using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;

    void Update()
    {
        if (UIManager.Instance != null && UIManager.Instance.currentState != UIManager.GameState.Playing)
        {
            foreach (char letter in Input.inputString)
            {
                if (char.IsLetter(letter))
                {
                    UIManager.Instance.TypeUIOption(char.ToLower(letter));
                }
            }
            return;
        }

        foreach (char letter in Input.inputString)
        {
            if (char.IsLetter(letter) && wordManager != null)
            {
                wordManager.TypeLetter(char.ToLower(letter));
            }
        }
    }
}