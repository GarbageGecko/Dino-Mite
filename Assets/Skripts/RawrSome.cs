using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RawrSome : MonoBehaviour
{
    public void OnNextLevel ()
    {
        SceneManager.LoadScene(2);
    }

    public void OnMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
