using UnityEngine;

public class KnifeCollision : MonoBehaviour
{
    public GameObject newPrefab; // The new prefab the bread will turn into

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the collider is tagged as "Bread"
        if (other.CompareTag("Bread"))
        {
            // Change bread to new prefab
            ChangeBread(other.gameObject);
        }
    }

    void ChangeBread(GameObject bread)
    {
        // Destroy the old bread object
        Destroy(bread);

        // Instantiate the new prefab in the bread's position and rotation
        Instantiate(newPrefab, bread.transform.position, bread.transform.rotation);
    }
}
