using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public DinoMovement Dino;
    public Meteorl1 Meteor;

    public spawner Spawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("DoSomething() wurde aufgerufen!");
         if (Time.time >= 6f){
        Spawn.setFalseSpawn();
        Meteor.setFalse();
         }
        Dino.setFalseDino();

       
    }

    void moveMeteor(){
        Meteor.GetComponent<Meteorl1>().setFalse();
    }

    void moveDino(){
        
    }
}

