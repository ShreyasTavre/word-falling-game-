using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public WordManager wordManager;

    [Header("Spawn Rate Settings")]
    public float initialDelay = 1.5f;
    public float minDelay = 0.4f;
    [Range(0.9f, 1.0f)]
    public float difficultyMultiplier = 0.995f;

    private float nextWordTime;
    private float currentDelay;

    void Start()
    {
        currentDelay = initialDelay;
        nextWordTime = Time.time + currentDelay;
    }

    void Update()
    {
        if (Time.time >= nextWordTime && this.enabled)
        {
            wordManager.AddWord();
            nextWordTime = Time.time + currentDelay;
            
            // Increase difficulty but clamp it within safe bounds
            currentDelay *= difficultyMultiplier;
            currentDelay = Mathf.Clamp(currentDelay, minDelay, initialDelay);
        }
    }
}