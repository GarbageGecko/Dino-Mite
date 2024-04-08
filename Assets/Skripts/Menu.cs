using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//menu skript

public class Menu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);              //Load Level 1
    }

    public void OnChangeLevel()
    {
        SceneManager.LoadScene(4);
    }
}
