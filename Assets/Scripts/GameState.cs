using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {


    bool gameHasEnded = false;
    
    public float restartDelay = 1f;

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GameOver");
            Invoke("GameOverScene", restartDelay);
        }
    }

   public void MainMenuScene()
    {
        FindObjectOfType<AudioManager>().StopMusic("GameOverTheme");
        FindObjectOfType<AudioManager>().StopMusic("WinTheme");
        FindObjectOfType<AudioManager>().PlayMusic("Theme");
        SceneManager.LoadScene("Menu");
    }

    public void GameOverScene()
    {
        FindObjectOfType<AudioManager>().PlayMusic("GameOverTheme");
        SceneManager.LoadScene("GameOverScene");
    }

    public void WinScene()
    {
        FindObjectOfType<AudioManager>().PlayMusic("WinTheme");
        SceneManager.LoadScene("Winning Screen");
    }

    public void RestartGame()
    {
        FindObjectOfType<AudioManager>().StopMusic("GameOverTheme");
        FindObjectOfType<AudioManager>().StopMusic("WinTheme");
        FindObjectOfType<AudioManager>().PlayMusic("Theme");
        SceneManager.LoadScene("MileStone6");
    }
    public void WinGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Win");
            Invoke("WinScene", 0);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
