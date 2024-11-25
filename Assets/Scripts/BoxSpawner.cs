using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab to spawn
    public Transform spawnPoint;    // Set a spawn point as a child of the box or elsewhere

    private bool canSpawn = true;   // Prevent multiple spawns on a single grab

    // This method is triggered by your VR interaction system when you try to pick up the object
    public void OnGrabAttempt()
    {
        if (canSpawn)
        {
            SpawnObject();
            canSpawn = false; // Prevents multiple spawns in quick succession
            Invoke("ResetSpawn", 0.5f); // Cooldown to allow spawning again
        }
    }

    void SpawnObject()
    {
        if (objectToSpawn != null && spawnPoint != null)
        {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }

    void ResetSpawn()
    {
        canSpawn = true;
    }
}
