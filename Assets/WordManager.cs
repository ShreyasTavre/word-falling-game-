using UnityEngine;
using System;  
using System.Collections.Generic;

public class WordManager : MonoBehaviour
{
    public List<Word> words;

    private bool hasActiveWord;
	private Word activeWord;

    public WordSpawner wordSpawner;



    public void AddWord()
    {
        Word word = new Word(Wordgenerator.GetRandomWord(),  wordSpawner.SpawnWord());
        Debug.Log(word.word);
        words.Add(word);
    }

    public void TypeLetter (char letter)
    {
        if (hasActiveWord)
		{
			if (activeWord.GetNextLetter() == letter)
			{
				activeWord.TypeLetter();
			}
		} else
		{
			foreach(Word word in words)
			{
				if (word.GetNextLetter() == letter)
				{
					activeWord = word;
					hasActiveWord = true;
					word.TypeLetter();
					break;
				}
			}
		}

		if (hasActiveWord && activeWord.WordTyped())
		{
			hasActiveWord = false;
			words.Remove(activeWord);
		}
	}

}

