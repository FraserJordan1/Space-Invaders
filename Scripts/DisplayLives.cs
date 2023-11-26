using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLives : MonoBehaviour
{
    [SerializeField]
    public Text livesText; // number of lives in numerical form
    private int _lives = 3; // initial lives

    // Start is called before the first frame update
    private void Start()
    {
        livesText = GetComponentInChildren<Text>();
        if (livesText == null)
        {
            Debug.LogError("No Text component found in children. (Lives)");
        }
        UpdateLives();
    }

    // Call method when player loses life
    public void LoseLife()
    {
        _lives--;
        UpdateLives();
        if (_lives <= 0)
        {
            Debug.Log("You Lose!");
        }
    }

    // Update text display of lives
    private void UpdateLives()
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {_lives}";
        }
    }
}
