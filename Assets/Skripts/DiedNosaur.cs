using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedNosaur : MonoBehaviour
{
   /*  public void OnPlayAgain ()
    {
        SceneManager.LoadScene(1);
    } */

    public void OnMenu ()
    {
        SceneManager.LoadScene(0);
    }

public void OnPlayAgain (){
    // Get the current active scene
        string currentScene = GameManager._activeSceneName;

        // Determine the next scene to load based on the current scene
        switch (currentScene.ToLower())
        {
            case "level1":
                SceneManager.LoadScene(1); 
                break;
            case "level2":
                SceneManager.LoadScene(2); 
                break;
            case "level3":
                SceneManager.LoadScene(3);
                break;
            default:
                 SceneManager.LoadScene(0);
                 Debug.Log("No next level defined for this level");
               
                break;}
}
}