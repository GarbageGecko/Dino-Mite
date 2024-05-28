using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RawrsomeL3 : MonoBehaviour
{
  public AudioSource src;
    public AudioClip winsound;
     void Start()
    {
        src.clip = winsound;
        src.Play();
    }
    public void OnMenu ()

    {
    
        SceneManager.LoadScene(0);
    }

      public void OnHighscore ()
    {
         SceneManager.LoadScene("HighScoreScene");
    }
}
