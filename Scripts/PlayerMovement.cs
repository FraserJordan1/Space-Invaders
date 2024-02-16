using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Basic Player Settings")]
    public float speed = 3.0f; // movement speed
    public float bulletSpeed = 6.0f; // speed of projectile movement
    public float fireCooldown = 0.5f; // cooldown between projectile shots
    private float nextFireTime = 0f; // time when the next shot can be fired

    [Header("Link to GUI")]
    public int score = 0;
    public int lives = 3;

    [Header("Objects")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public DisplayLives displayLives;
    public DisplayScore displayScore;

    [Header("Sound Effects")]
    public AudioClip shootingSound;
    public AudioClip dyingSound;
    private AudioSource shootingAudioSource;
    private AudioSource dyingAudioSource;

    // If player gets hit
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            PlayerLosesLife();
        }
    }

    private void Start()
    {
        shootingAudioSource = gameObject.GetComponent<AudioSource>();
        dyingAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // basic player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * speed * Time.deltaTime, 0);
        transform.Translate(movement);

        // Fire projectile from point of player
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            nextFireTime = Time.time + fireCooldown;
            shootingAudioSource.PlayOneShot(shootingSound, 0.5f);
        }
    }

    // make dying sound
    private void PlayDyingSound()
    {
        if (dyingSound != null)
        {
            dyingAudioSource.PlayOneShot(dyingSound, 0.5f);
        }
    }

    // Player loses life
    public void PlayerLosesLife()
    {
        lives--;
        if (lives <= 0 && displayLives != null)
        {
            Debug.Log("You Lose!");
            PlayDyingSound();
            SceneManager.LoadScene("GameOver");
        }
        displayLives.LoseLife();
    }

    // Player kills an enemy
    public void PlayerEarnsPoint()
    {
        score++;
        if (displayScore != null)
        {
            displayScore.AddPoint();
        }
    }
}
