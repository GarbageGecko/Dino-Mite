using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RawrSome : MonoBehaviour


{
    public AudioSource src;
    public AudioClip winsound;

void Start()
    {
    src.clip=winsound;
             src.Play();
             }
    public void OnNextLevel ()
    {
        // Get the current active scene
        string currentScene = GameManager._activeSceneName;

        // Determine the next scene to load based on the current scene
        switch (currentScene.ToLower())
        {
            case "level1":
                SceneManager.LoadScene(11); // Load Level 2 if Level 1 was completed
                break;
            case "level2":
                SceneManager.LoadScene(12); // Load Level 3 if Level 2 was completed
                break;
            default:
                 SceneManager.LoadScene(0);
                 Debug.Log("No next level defined for this level");
               
                break;
        }
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
