using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Das Objekt, das gespawnt werden soll
    public Transform[] spawnPoints; // Die Positionen, an denen das Objekt gespawnt werden kann
    public bool hasspawned= false;
    private float delta=0f;
    void Start()
    {
       
    }
    void Update()
    {
        delta= delta+Time.deltaTime;
        if (delta > 3f){
        SpawnObject();
        delta=0f;
        }
    }

    void SpawnObject()
    {
        if(hasspawned==false){
        // Zufällige Auswahl eines der Spawn-Punkte
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Erzeugen des Objekts an der ausgewählten Position
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        hasspawned=true;
        }
    }

    public void setFalseSpawn(){

    hasspawned=false;
    Debug.Log("setfalsespawn() wurde aufgerufen!");
    Debug.Log(hasspawned);
    }
}
