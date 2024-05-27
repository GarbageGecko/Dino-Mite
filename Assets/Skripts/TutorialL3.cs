using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialL3 : MonoBehaviour
{
    public void OnWeiter ()
    {
        if(Menu.tutorial ==true){
            Menu.tutorial=false;
            SceneManager.LoadScene(0);

        }else{
        SceneManager.LoadScene(3);
        }
    }
}

