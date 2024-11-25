using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RakeGroundInteraction : MonoBehaviour
{
    public GameObject drySoilPrefab;            // The dry soil prefab
    public GameObject wetSoilPrefab;            // The wet soil prefab
    public GameObject terrainObject;            // Reference to the terrain object
    public float spawnHeightOffset = 0.01f;     // Offset to avoid clipping
    public float gridSize = 1f;                 // Size of each grid cell
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == terrainObject && IsRakeHeldByPlayer())
        {
            Vector3 touchPoint = transform.position;

            Vector3 gridPosition = new Vector3(
                Mathf.Round(touchPoint.x / gridSize) * gridSize,
                touchPoint.y,
                Mathf.Round(touchPoint.z / gridSize) * gridSize
            );

            Terrain terrain = terrainObject.GetComponent<Terrain>();
            gridPosition.y = terrain.SampleHeight(gridPosition) + spawnHeightOffset;

            if (!occupiedPositions.Contains(gridPosition))
            {
                SpawnSoilPatch(gridPosition);
                occupiedPositions.Add(gridPosition);
            }
        }
    }

    private bool IsRakeHeldByPlayer()
    {
        return true; // Placeholder logic for rake possession check
    }

    private void SpawnSoilPatch(Vector3 position)
    {
        GameObject soilPatch = Instantiate(drySoilPrefab, position, Quaternion.identity);

        Vector3 normal = terrainObject.GetComponent<Terrain>().terrainData.GetInterpolatedNormal(
            position.x / terrainObject.GetComponent<Terrain>().terrainData.size.x,
            position.z / terrainObject.GetComponent<Terrain>().terrainData.size.z
        );
        soilPatch.transform.rotation = Quaternion.FromToRotation(Vector3.up, normal);

        StartCoroutine(RemoveSoilPatchAfterDelay(soilPatch, position, 60f));
    }

    private IEnumerator RemoveSoilPatchAfterDelay(GameObject soilPatch, Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);

        SoilPatch soilPatchComponent = soilPatch.GetComponent<SoilPatch>();
        if (soilPatchComponent != null && !soilPatchComponent.isWet)
        {
            Destroy(soilPatch);
            occupiedPositions.Remove(position);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water"))
        {
            // Check if the collided object has a SoilPatch component
            SoilPatch soilPatch = other.GetComponentInParent<SoilPatch>();
            if (soilPatch != null && !soilPatch.isWet)
            {
                soilPatch.isWet = true; // Mark the soil as wet to avoid duplicate watering

                // Get the position and rotation of the current dry soil patch
                Vector3 position = soilPatch.transform.position;
                Quaternion rotation = soilPatch.transform.rotation;

                // Instantiate the wet soil object at the same position and rotation
                GameObject wetSoil = Instantiate(wetSoilPrefab, position, rotation);

                // Destroy the original dry soil object
                Destroy(soilPatch.gameObject);
            }
        }
    }
}
