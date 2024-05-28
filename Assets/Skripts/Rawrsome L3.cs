using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RawrsomeL3 : MonoBehaviour
{
    public void OnMenu ()
    {
        SceneManager.LoadScene(0);
    }

      public void OnHighscore ()
    {
         SceneManager.LoadScene("HighScoreScene");
    }
}
