using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuNavigation : MonoBehaviour
{
    [SerializeField]
    public Button startButton;
    public Button exitButton;
    public RectTransform selectArrow;

    private Button _selectButton;

    private void Start()
    {
        _selectButton = startButton; // start with Start button
        UpdateArrowPosition();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            ToggleSelect();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_selectButton == startButton)
            {
                StartGame();
            }
            else if (_selectButton == exitButton)
            {
                ExitGame();
            }
        }
    }

    // toggle button to select button that is not selected
    public void ToggleSelect()
    {
        _selectButton = _selectButton == startButton ? exitButton : startButton;
        UpdateArrowPosition();
    }

    // have arrow mark next to "hovering select
    public void UpdateArrowPosition()
    {
        selectArrow.position = new Vector3(
            selectArrow.position.x,
            _selectButton.transform.position.y, 
            _selectButton.transform.position.z
        );
    }

    // start the game
    public void StartGame()
    {
        SceneManager.LoadScene("OngoingGame");
    }

    // terminate game
    public void ExitGame()
    {
        Application.Quit();
    }

}
