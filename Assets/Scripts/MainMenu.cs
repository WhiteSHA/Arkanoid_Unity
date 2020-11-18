using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool gameIsStarted = false;
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");

        gameIsStarted = true;
        GlobalVariables.lives = 5;
        GlobalVariables.level = 0;
        GlobalVariables.score = 0;
        GlobalVariables.countOfVblocks = 0;
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
