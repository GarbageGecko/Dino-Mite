using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial3 : MonoBehaviour
{
    public void OnWeiter ()
    {
        if(Menu.tutorial ==true){
            SceneManager.LoadScene(11);

        }else{
        SceneManager.LoadScene(1);
        }
    }
}

