using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class WordManager : MonoBehaviour
{
    [SerializeField] private WordSpawner wordSpawner;
    // We don't need the bulletPrefab field here anymore
    // [SerializeField] private GameObject bulletPrefab;
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
        if (activeWord != null)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        else
        {
            activeWord = words.FirstOrDefault(word => word.GetNextLetter() == letter);
            if (activeWord != null)
            {
                activeWord.TypeLetter();
            }
        }

        if (activeWord != null && activeWord.WordTyped())
        {
            words.Remove(activeWord);
            ShootAtWord(activeWord);
            activeWord = null;
        }
    }

    void ShootAtWord(Word wordToShoot)
    {
        WordDisplay targetDisplay = wordToShoot.GetWordDisplay();
        if (targetDisplay != null)
        {
            // --- THIS IS THE CHANGED PART ---
            // Instead of Instantiate, we spawn from the pool using a "tag"
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