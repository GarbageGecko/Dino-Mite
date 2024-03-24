using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoMovement : MonoBehaviour
{
  
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit
    public float moveDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void moveDino(){
         if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        // Bewegung nach rechts bei Drücken der rechten Pfeiltaste
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }


    }
    public void MoveLeft()
    {
        // Bewegung nach links um die festgelegte Distanz
        transform.Translate(Vector3.left * moveDistance * moveSpeed * Time.deltaTime);

    }

    public void MoveRight()
    {
        // Bewegung nach rechts um die festgelegte Distanz
        transform.Translate(Vector3.right * moveDistance * moveSpeed * Time.deltaTime);
    }

}
