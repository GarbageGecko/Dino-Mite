using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

static public bool tutorial;
    public void OnPlayButton()
    {
        SceneManager.LoadScene(7);              //Load Level 1
    }

    public void OnChangeLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void OnTutorial()
    {tutorial=true;
        SceneManager.LoadScene(7);
        
    }

    public void OnHighscore ()
    {
         SceneManager.LoadScene("HighScoreScene");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
