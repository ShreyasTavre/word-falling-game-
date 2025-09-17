using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 25f;
    
    private WordDisplay targetWordDisplay;
    private Rigidbody2D rb;
    private Camera mainCamera;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        targetWordDisplay = null;
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
            gameObject.SetActive(false);
            return;
        }

        if (mainCamera != null)
        {
            Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
            if (screenPoint.x < -0.1f || screenPoint.x > 1.1f || screenPoint.y < -0.1f || screenPoint.y > 1.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        WordDisplay hitWord = other.GetComponent<WordDisplay>();
        if (hitWord != null && hitWord == targetWordDisplay)
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}