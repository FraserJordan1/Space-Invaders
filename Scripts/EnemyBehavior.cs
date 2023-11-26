using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [Header("Basic Configuration")]
    public float speed = 2f;
    public float boundaryOffset = 0f;
    public Camera mainCamera;

    [Header("Audio Settings")]
    public AudioClip dyingSound;
    private AudioSource _dyingAudioSource;

    private float _leftBoundary;
    private float _rightBoundary;

    // need the enemy to go downward for one second. This is to keep track.
    private float _moveDownDuration = 0.25f;
    private float _moveDownTimer;

    private enum State { MovingLeft, MovingRight, MovingDown };
    private State _currState;
    private State _prevState;

    // when enemy gets hit by bullet
    // private void OnTriggerEnter2D(Collider2D collision)
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");
        if (collision.collider.CompareTag("Bullet"))
        {
            PlayDeathAudio();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        _dyingAudioSource = GetComponent<AudioSource>();
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found in enemy model.");
            return;
        }
        _leftBoundary = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).x;
        _rightBoundary = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane)).x;

        // Apply offset
        _leftBoundary += (2.5f + boundaryOffset);
        _rightBoundary += 2.5f;
        _rightBoundary -= boundaryOffset;

        _currState = State.MovingLeft;
        _prevState = State.MovingLeft;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_currState)
        {
            case State.MovingLeft:
                MoveLeft();
                break;
            case State.MovingRight:
                MoveRight();
                break;
            case State.MovingDown:
                MoveDown();
                break;
        }
    }

    // play sound when enemy dies
    private void PlayDeathAudio()
    {
        if (dyingSound != null)
        {
            _dyingAudioSource.PlayOneShot(dyingSound, 0.5f);
        }
    }

    // move to the left until it hits the left boundary
    private void MoveLeft()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < _leftBoundary)
        {
            _prevState = State.MovingLeft;
            _currState = State.MovingDown;
            _moveDownTimer =  _moveDownDuration; // reset
        }
    }

    // move down for one second
    private void MoveDown()
    {
        if (_moveDownTimer > 0)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            _moveDownTimer -= Time.deltaTime;
        }
        else
        {
            if (_prevState == State.MovingLeft)
            {
                _currState = State.MovingRight;
            }
            else if (_prevState == State.MovingRight)
            {
                _currState = State.MovingLeft;
            }
        }
    }

    // move to the right until it hits the right boundary
    private void MoveRight()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (transform.position.x > _rightBoundary)
        {
            _prevState = State.MovingRight;
            _currState = State.MovingDown;
            _moveDownTimer = _moveDownDuration;
        }
    }

}
