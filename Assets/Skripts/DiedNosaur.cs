using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiedNosaur : MonoBehaviour
{
    public void OnPlayAgain ()
    {
        SceneManager.LoadScene(1);
    }

    public void OnMenu ()
    {
        SceneManager.LoadScene(0);
    }
}
