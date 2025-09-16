using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    public GameObject wordPrefab;
    public Transform wordCanvas; // The canvas to spawn words under

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public WordDisplay SpawnWord()
    {
        // Calculate screen boundaries in world coordinates
        Vector3 leftBound = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightBound = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        
        float padding = 1.0f;
        float spawnY = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y + padding;

        Vector3 randomPosition = new Vector3(Random.Range(leftBound.x + padding, rightBound.x - padding), spawnY);

        GameObject wordObj = Instantiate(wordPrefab, randomPosition, Quaternion.identity, wordCanvas);
        return wordObj.GetComponent<WordDisplay>();
    }
}