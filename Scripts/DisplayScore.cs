using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{

    [SerializeField]
    public Text scoreText;
    private int _score;

    // Start is called before the first frame update
    private void Start()
    {
        scoreText = GetComponentInChildren<Text>();
        UpdateScore();
    }

    // add to total score
    public void AddPoint()
    {
        _score++;
        UpdateScore();
    }

    // update score of player
    private void UpdateScore()
    {
        _score++;
        if (scoreText != null)
        {
            scoreText.text = $"Score: {_score}";
        }
    }
}
