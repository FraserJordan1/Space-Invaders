using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// logic for bullet movement and ways of how it acts
public class BulletMovement : MonoBehaviour
{
    [Header("Bullet Parameters")]
    public float speed = 5f; // bullet speed
    public float maxYPosition = 300f; // set y position of when it gets destroyed

    private Rigidbody _rb;

    // Destroy self when hit
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<PlayerMovement>().PlayerEarnsPoint();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerMovement>().PlayerLosesLife();
        }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        // rb.velocity = transform.up * speed;   
        _rb.velocity = Vector2.up * speed; 
        _rb.useGravity = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.y >= maxYPosition)
        {
            Destroy(gameObject); // Destroys itself once it reaches the limit
        }
    }
}
