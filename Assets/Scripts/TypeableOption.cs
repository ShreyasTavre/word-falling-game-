using UnityEngine;
using UnityEngine.Events;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypeableOption : MonoBehaviour
{
    public string optionWord;
    public UnityEvent onWordTyped;

    private TextMeshProUGUI textDisplay;
    private int typeIndex;
    private readonly string originalColorHex = "#FFFFFF"; // White
    private readonly string typedColorHex = "#FF0000";    // Red

    void Awake()
    {
        textDisplay = GetComponent<TextMeshProUGUI>();
        optionWord = optionWord.ToLower();
        Reset();
    }

    public void Reset()
    {
        typeIndex = 0;
        if (textDisplay != null)
        {
            textDisplay.text = $"<color={originalColorHex}>{optionWord}</color>";
        }
    }

    public char GetNextLetter()
    {
        return optionWord[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
        string typedPart = optionWord.Substring(0, typeIndex);
        string untypedPart = optionWord.Substring(typeIndex);
        
        if (textDisplay != null)
        {
            textDisplay.text = $"<color={typedColorHex}>{typedPart}</color><color={originalColorHex}>{untypedPart}</color>";
        }
    }

    public bool IsTyped()
    {
        return (typeIndex >= optionWord.Length);
    }

    public void InvokeTypedEvent()
    {
        onWordTyped.Invoke();
    }
}