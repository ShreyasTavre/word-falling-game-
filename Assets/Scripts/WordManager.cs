using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WordManager : MonoBehaviour
{
    [SerializeField] private WordSpawner wordSpawner;
    [SerializeField] private Transform firePoint;

    private List<Word> words = new List<Word>();
    private Word activeWord;

    public void AddWord()
    {
        Word word = new Word(Wordgenerator.Instance.GetRandomWord(), wordSpawner.SpawnWord());
        words.Add(word);
    }

    public void TypeLetter(char letter)
    {
        // If we already have an active word, we can only type letters for that word.
        if (activeWord != null)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        // If there's no active word, find a new one that starts with the typed letter.
        else
        {
            // Find the first word in the list that matches the typed letter.
            activeWord = words.FirstOrDefault(word => word.GetNextLetter() == letter);
            
            if (activeWord != null)
            {
                activeWord.TypeLetter();
            }
        }

        // If the active word is fully typed, clear it so we can select a new one.
        if (activeWord != null && activeWord.WordTyped())
        {
            if(UIManager.Instance != null)
            {
                UIManager.Instance.AddScore(1);
            }

            words.Remove(activeWord);
            ShootAtWord(activeWord);
            activeWord = null;
        }
    }

    void ShootAtWord(Word wordToShoot)
    {
        if (firePoint == null)
        {
            Debug.LogError("FirePoint is not assigned in the WordManager Inspector!");
            return;
        }
        
        WordDisplay targetDisplay = wordToShoot.GetWordDisplay();
        if (targetDisplay != null)
        {
            GameObject bullet = ObjectPooler.Instance.SpawnFromPool("bullet", firePoint.position, Quaternion.identity);
            
            if (bullet != null)
            {
                BulletController bulletController = bullet.GetComponent<BulletController>();
                if (bulletController != null)
                {
                    bulletController.SetTarget(targetDisplay);
                }
            }
        }
    }
}