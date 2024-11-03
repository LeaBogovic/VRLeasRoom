using UnityEngine;

public class SoilPatch : MonoBehaviour
{
    public Material drySoilMaterial; // Material for dry soil
    public Material wetSoilMaterial; // Material for wet soil

    private Renderer soilRenderer;
    private bool isWet = false;

    private void Start()
    {
        // Get the Renderer component and set the initial material to dry soil
        soilRenderer = GetComponent<Renderer>();
        soilRenderer.material = drySoilMaterial;
    }

    private void OnParticleCollision(GameObject other)
    {
        // Check if the particle collision is with water particles from the watering can
        if (other.CompareTag("Water") && !isWet)
        {
            ChangeToWetSoil();
        }
    }

    private void ChangeToWetSoil()
    {
        isWet = true;
        soilRenderer.material = wetSoilMaterial;
    }
}
