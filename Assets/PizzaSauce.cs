using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSauce : MonoBehaviour
{
    public GameObject newPrefab;
    private HashSet<GameObject> processedObjects = new HashSet<GameObject>(); // Track processed objects

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is a PizzaBread and hasn't already been processed
        if (other.CompareTag("PizzaBread") && !processedObjects.Contains(other.gameObject))
        {
            processedObjects.Add(other.gameObject); // Mark as processed
            ChangeBread(other.gameObject);
        }
    }

    void ChangeBread(GameObject bread)
    {
        // Preserve the position and rotation of the bread
        Vector3 breadPosition = bread.transform.position;
        Quaternion breadRotation = bread.transform.rotation;

        Destroy(bread);

        // Instantiate the new prefab
        GameObject newBread = Instantiate(newPrefab, breadPosition, breadRotation);

        // Change the tag or state of the new prefab to avoid retriggering
        if (newBread.CompareTag("PizzaBread"))
        {
            newBread.tag = "SaucedBread"; // Update the tag to prevent further sauce interactions
        }
    }
}
