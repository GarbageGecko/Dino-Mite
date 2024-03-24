using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorl1 : MonoBehaviour
{

    public float moveSpeed = 20f; // Bewegungsgeschwindigkeit nach unten

 void Start()
    {
        
    }
    void Update()
    {
  
    }
    public void moveMeteors(){
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
