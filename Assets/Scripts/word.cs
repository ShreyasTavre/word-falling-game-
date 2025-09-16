using UnityEngine;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;
    private WordDisplay display;

    private readonly string typedColorHex = "#FF0000"; // Red

    public Word(string _word, WordDisplay _display)
    {
        word = _word;
        typeIndex = 0;
        display = _display;
        display.SetWord(word);
    }

    public char GetNextLetter() => word[typeIndex];

    public void TypeLetter()
    {
        typeIndex++;
        string typedPart = word.Substring(0, typeIndex);
        string untypedPart = word.Substring(typeIndex);
        display.UpdateText($"<color={typedColorHex}>{typedPart}</color>{untypedPart}");
    }

    public bool WordTyped() => (typeIndex >= word.Length);

    // Provide direct access to the WordDisplay component
    public WordDisplay GetWordDisplay() => display;
}