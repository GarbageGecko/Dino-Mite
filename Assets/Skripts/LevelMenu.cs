using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//level menu skript
public class LevelMenu : MonoBehaviour
{
    public void OnLevel1 ()
    {
        SceneManager.LoadScene(1);
    }

    public void OnLevel2 ()
    {
        SceneManager.LoadScene(2);
    }

    public void OnLevel3 ()
    {
        SceneManager.LoadScene(3);
    }
}
