using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadNextScreen();
        }       
    }

    private void LoadNextScreen()
    {
        SceneManager.LoadScene("TitleMenu");
    }

}
