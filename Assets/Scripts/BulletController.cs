using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 25f;
    
    private WordDisplay targetWordDisplay;
    private Rigidbody2D rb;
    private Camera mainCamera;

    // This is a new method that gets called when the object is enabled
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }
    
    public void SetTarget(WordDisplay target)
    {
        targetWordDisplay = target;
    }

    void Update()
    {
        if (targetWordDisplay != null && targetWordDisplay.gameObject.activeInHierarchy)
        {
            Vector2 moveDirection = (targetWordDisplay.transform.position - transform.position).normalized;
            rb.linearVelocity = moveDirection * speed;
        }
        else
        {
            // If the target is gone, return to the pool
            gameObject.SetActive(false);
            return;
        }

        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPoint.x < -0.1f || screenPoint.x > 1.1f || screenPoint.y < -0.1f || screenPoint.y > 1.1f)
        {
            // If off-screen, return to the pool
            gameObject.SetActive(false);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        WordDisplay hitWord = other.GetComponent<WordDisplay>();
        if (hitWord != null && hitWord == targetWordDisplay)
        {
            // Destroy the word, but return the bullet to the pool
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}