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
        // Bewegung nach links
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !keyDown)
        {
            keyDown = true;
            Dino.MoveLeft();
            hasmoved = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && hasmoved == false)
        {
            MeteorMovement();
            hasmoved = true;
            keyDown = false;
        }

        // Bewegung nach rechts
        if (Input.GetKeyDown(KeyCode.RightArrow) && !keyDown)
        {
            keyDown = true;
            Dino.MoveRight();
            hasmoved = false;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && hasmoved == false)
        {
            MeteorMovement();
            hasmoved = true;
            keyDown = false;
        }
    }

    public void MeteorMovement()
    {
        Spawn.SpawnObject();
        Meteor.moveMeteors();
    }
}
