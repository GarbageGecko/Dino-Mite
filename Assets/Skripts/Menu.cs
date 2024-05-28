using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

static public bool tutorial;
 static public bool seenTutorial = false;

    public void OnPlayButton()
    {
        if (!seenTutorial)
        {
            SceneManager.LoadScene(7);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        seenTutorial = true;
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
