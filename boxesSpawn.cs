using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boxesSpawn : MonoBehaviour
{ // NO CREO QUE SE USE
    public GameObject boxPrefab; // Assign this in the Inspector
    public int numberOfBoxes = 60;
    public float mapWidth = 50f; // Adjust these values based on the size of your map
    public float mapLength = 50f;
    public float maxSpawnHeight = 12.5f; // Maximum Y-axis value to check for spawn

    void Start()
    {
        Debug.Log("Entro al start");
        SpawnRandomBoxes();
    }

    void SpawnRandomBoxes()
    {
        for (int i = 0; i < numberOfBoxes; i++)
        {
            Vector3 randomPoint = GetRandomPointOnNavMesh();
            if (randomPoint != Vector3.zero) // Vector3.zero is our indicator of a failed attempt
            {
                Debug.Log("entro al if de instanciar");
                Instantiate(boxPrefab, randomPoint, Quaternion.identity);
            }
        }
    }

    Vector3 GetRandomPointOnNavMesh()
    {
        for (int i = 0; i < 30; i++) // Try up to 30 times for each spawn
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(-mapWidth / 2, mapWidth / 2),
                12.2f,
                Random.Range(-mapLength / 2, mapLength / 2)
            );
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, maxSpawnHeight, NavMesh.AllAreas))
            {
                return hit.position; // Successfully found a point on the NavMesh
            }
        }
        return Vector3.zero; // Indicate failure to find a valid point
    }

}
