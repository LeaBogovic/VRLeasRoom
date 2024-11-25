using UnityEngine;
using System.Collections.Generic;

public class SeedPlacement : MonoBehaviour
{
    public GameObject seedPrefab;                 // The seed prefab to spawn
    public GameObject terrainObject;              // Reference to the terrain object
    public float gridSize = 1f;                   // Size of each grid cell

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Check for seed placement input (e.g., left mouse button)
        {
            PlaceSeed();
        }
    }

    private void PlaceSeed()
    {
        // Get the position of the player's hand or where they are aiming
        Vector3 touchPoint = transform.position; // Change this to the position where you want to check

        // Round the position to the nearest grid cell
        Vector3 gridPosition = new Vector3(
            Mathf.Round(touchPoint.x / gridSize) * gridSize,
            touchPoint.y, // Keep the y position as it is
            Mathf.Round(touchPoint.z / gridSize) * gridSize
        );

        // Check if a soil patch exists at the grid position
        Collider[] hitColliders = Physics.OverlapSphere(gridPosition, 0.1f); // Small radius to detect soil patch

        bool soilPatchExists = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<SoilPatch>() != null)
            {
                soilPatchExists = true;
                break; // Stop checking if we found a soil patch
            }
        }

        // Place the seed if a soil patch exists
        if (soilPatchExists)
        {
            // Adjust the Y position to match the terrain height
            gridPosition.y = terrainObject.GetComponent<Terrain>().SampleHeight(gridPosition) + 0.01f; // Small offset

            // Instantiate the seed at the grid position
            Instantiate(seedPrefab, gridPosition, Quaternion.identity);
        }
    }
}
