using UnityEngine;
using System;  
using System.Collections.Generic;

[System.Serializable]
public class Word
{
    public string word;
    private int typeIndex;   // <-- Add this field

    WordDisplay display;

	public Word (string _word, WordDisplay _display)
	{
		word = _word;
		typeIndex = 0;

		display = _display;
		display.SetWord(word);
	}

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
        display.RemoveLetter();
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            display.RemoveWord();
            // you can reset or handle when word is fully typed
        }
        return wordTyped;
    }
}
