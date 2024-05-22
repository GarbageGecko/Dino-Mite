using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialL2 : MonoBehaviour
{
    public void OnWeiter ()
    {
        SceneManager.LoadScene(2);
    }
}

