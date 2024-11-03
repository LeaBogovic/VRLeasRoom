using UnityEngine;
using System.Collections.Generic;

public class RakeGroundInteraction : MonoBehaviour
{
    public GameObject soilPatchPrefab;          // The soil patch prefab to spawn
    public GameObject terrainObject;            // Reference to the terrain object
    public float spawnHeightOffset = 0.01f;     // Offset to avoid clipping
    public float gridSize = 1f;                  // Size of each grid cell

    // A HashSet to keep track of occupied positions
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == terrainObject && IsRakeHeldByPlayer())
        {
            // Get the position where the rake touches the ground
            Vector3 touchPoint = transform.position;

            // Round the position to the nearest grid cell
            Vector3 gridPosition = new Vector3(
                Mathf.Round(touchPoint.x / gridSize) * gridSize,
                touchPoint.y,  // Keep the y position as it is
                Mathf.Round(touchPoint.z / gridSize) * gridSize
            );

            // Adjust the gridPosition's Y to match the terrain's height
            gridPosition.y = terrainObject.GetComponent<Terrain>().SampleHeight(gridPosition) + spawnHeightOffset;

            // Check if the position is already occupied
            if (!occupiedPositions.Contains(gridPosition))
            {
                // Instantiate a soil patch at the grid position
                SpawnSoilPatch(gridPosition);
                occupiedPositions.Add(gridPosition);  // Mark this position as occupied
            }
        }
    }

    private bool IsRakeHeldByPlayer()
    {
        // Placeholder for actual logic to check if the rake is in the player’s hand
        return true;
    }

    private void SpawnSoilPatch(Vector3 position)
    {
        // Instantiate the soil patch at the adjusted position and set it to face upwards
        GameObject soilPatch = Instantiate(soilPatchPrefab, position, Quaternion.Euler(90, 0, 0));
    }
}
