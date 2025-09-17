using UnityEngine;

public class WordInput : MonoBehaviour
{
    public WordManager wordManager;

    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            if (char.IsLetter(letter))
            {
                wordManager.TypeLetter(char.ToLower(letter));
            }
        }
    }
}