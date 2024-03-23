using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorl1 : MonoBehaviour
{
    public static Meteorl1 Instance;
    public float moveSpeed = 20f; // Bewegungsgeschwindigkeit nach unten
    private bool hasMoved = false;

    void Update()
    {
        
           if (!hasMoved && Time.time >= 2f)
        {
            MoveDown();
            hasMoved = true;
        }
        // Überprüfen, ob das Objekt sich noch nicht bewegt hat und zwei Sekunden vergangen sind
       
    }


    void MoveDown()
    {
        // Bewegung um 20 Einheiten nach unten
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    public void setFalse(){
        hasMoved=false;
        Debug.Log("setfalse() wurde aufgerufen!");
    
    }
}
