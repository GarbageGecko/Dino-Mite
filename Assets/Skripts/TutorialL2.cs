using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialL2 : MonoBehaviour
{
   public void OnWeiter ()
    {
        if(Menu.tutorial ==true){
            SceneManager.LoadScene(12);

        }else{
        SceneManager.LoadScene(2);
        }
    }
}

