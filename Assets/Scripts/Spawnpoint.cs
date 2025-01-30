using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public static Vector3 lastSpawnPosition;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player touches the spawn point
        if (other.CompareTag("Player"))
        {
            // Update the last spawn position to this spawn point's position
            lastSpawnPosition = transform.position;
            Debug.Log("Spawn point activated at: " + lastSpawnPosition);
        }
    }
}
