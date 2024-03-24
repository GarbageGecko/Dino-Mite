using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Das Objekt, das gespawnt werden soll
    public Transform[] spawnPoints; // Die Positionen, an denen das Objekt gespawnt werden kann
    
    void Start()
    {
       
    }
    void Update()
    {

    }

    public void SpawnObject()
    {
        // Zufällige Auswahl eines der Spawn-Punkte
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Erzeugen des Objekts an der ausgewählten Position
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
    }

}
