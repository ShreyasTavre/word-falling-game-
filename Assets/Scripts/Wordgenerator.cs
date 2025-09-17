using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Wordgenerator : MonoBehaviour
{
    // --- Singleton Pattern ---
    private static Wordgenerator _instance;
    public static Wordgenerator Instance
    {
        get
        {
            if (_instance == null) Debug.LogError("WordGenerator is NULL.");
            return _instance;
        }
    }

    [Tooltip("The .txt file from your Resources folder containing words, one per line.")]
    [SerializeField] private TextAsset wordListSource;
    
    private List<string> wordList;
    private string lastWord = "";

    void Awake()
    {
        // Set up the singleton instance, ensuring only one exists
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Load words from the TextAsset, trim whitespace, and handle potential errors
        if (wordListSource != null)
        {
            wordList = wordListSource.text.Split('\n').Select(word => word.Trim()).ToList();
            Debug.Log($"Word list loaded successfully with {wordList.Count} words.");
        }
        else
        {
            Debug.LogError("WordListSource TextAsset is not assigned in the WordGenerator's Inspector!");
            wordList = new List<string>(); // Create an empty list to prevent further errors
        }
    }

    public string GetRandomWord()
    {
        if (wordList == null || wordList.Count == 0)
        {
            Debug.LogError("Word list is empty! Ensure your words.txt file is not empty and is assigned.");
            return "ERROR";
        }
        
        string randomWord;
        
        // Ensure the same word isn't picked twice in a row if possible
        do
        {
            int randomIndex = Random.Range(0, wordList.Count);
            randomWord = wordList[randomIndex];
        } while (randomWord == lastWord && wordList.Count > 1);

        lastWord = randomWord;
        return randomWord;
    }
}