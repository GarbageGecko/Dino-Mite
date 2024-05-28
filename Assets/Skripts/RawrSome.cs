using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RawrSome : MonoBehaviour


{
    public AudioSource src;
    public AudioClip winsound;
    static public bool seenTutorial2 = false;
   static public bool seenTutorial3 = false;

    void Start()
    {
        src.clip = winsound;
        src.Play();
    }
    public void OnNextLevel()
    {
        // Get the current active scene
        string currentScene = GameManager._activeSceneName;

        // Determine the next scene to load based on the current scene
        switch (currentScene.ToLower())
        {
            case "level1":
                if (!seenTutorial2)
                {
                    SceneManager.LoadScene(11); // Load Level 2 if Level 1 was completed
                }
                else { SceneManager.LoadScene(2); }
                seenTutorial2=true;
                break;
            case "level2":
             if (!seenTutorial3)
                {
                    SceneManager.LoadScene(12); // Load Level 2 if Level 1 was completed
                }
                else { SceneManager.LoadScene(3); }
              seenTutorial3=true;
                break;
            default:
                SceneManager.LoadScene(0);
                Debug.Log("No next level defined for this level");

                break;
        }
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnHighscore()
    {
        SceneManager.LoadScene("HighScoreScene");
    }
}
