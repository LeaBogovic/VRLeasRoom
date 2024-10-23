using UnityEngine;

public class HoeTerrainChanger : MonoBehaviour
{
    // Index of the brown texture in the terrain's texture layers (adjust if necessary)
    public int brownTextureIndex = 1;

    // Size of the area affected when the hoe touches the ground
    public int areaSize = 3;

    // Reference to the terrain
    private Terrain terrain;
    private TerrainData terrainData;

    void Start()
    {
        // Get the active terrain and its data
        terrain = Terrain.activeTerrain;
        terrainData = terrain.terrainData;
    }

    // Detect when the hoe enters a collision with the ground
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("terrain"))
        {
            // Change the terrain texture at the point where the hoe touches
            Vector3 touchPoint = other.ClosestPoint(transform.position);
            ChangeTerrainTexture(touchPoint);
        }
    }

    // Changes the terrain texture at the given world position
    void ChangeTerrainTexture(Vector3 position)
    {
        // Convert world position to terrain local position
        Vector3 terrainPos = terrain.transform.InverseTransformPoint(position);

        // Get the coordinates on the texture map (heightmap resolution)
        int mapX = (int)((terrainPos.x / terrainData.size.x) * terrainData.alphamapWidth);
        int mapZ = (int)((terrainPos.z / terrainData.size.z) * terrainData.alphamapHeight);

        // Get the alpha map for the affected area
        float[,,] alphaMap = terrainData.GetAlphamaps(mapX, mapZ, areaSize, areaSize);

        // Modify the alpha map to set the brown texture in the specified area
        for (int x = 0; x < areaSize; x++)
        {
            for (int z = 0; z < areaSize; z++)
            {
                // Ensure brown texture is dominant
                alphaMap[x, z, brownTextureIndex] = 1.0f;

                // Set other textures to zero (optional, depending on blending requirements)
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {
                    if (i != brownTextureIndex)
                    {
                        alphaMap[x, z, i] = 0.0f;
                    }
                }
            }
        }

        // Apply the modified alpha map to the terrain
        terrainData.SetAlphamaps(mapX, mapZ, alphaMap);
    }
}
