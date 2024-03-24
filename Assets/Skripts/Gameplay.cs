using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public DinoMovement Dino;
    public Meteorl1 Meteor;
    public spawner Spawn;


    private bool hasmoved = true;
    private bool keyDown = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !keyDown)
        {
            keyDown = true; // Die Taste wurde gedrückt, sodass die Aktion nur einmal ausgeführt wird
            // Führe hier die gewünschte Aktion aus
            Dino.MoveLeft();
            hasmoved=false;
        }

        // Überprüfe, ob die Taste losgelassen wurde
        if (Input.GetKeyUp(KeyCode.LeftArrow) && hasmoved==false)
        {
            MeteorMovement();
            hasmoved=true;
            keyDown = false; // Die Taste wurde losgelassen, sodass die Aktion erneut ausgeführt werden kann
        }


       
    }

    public void MeteorMovement(){
        Spawn.SpawnObject();
        Meteor.moveMeteors();
    }


}

