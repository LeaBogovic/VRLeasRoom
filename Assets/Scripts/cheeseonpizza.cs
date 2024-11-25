using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseonpizza : MonoBehaviour
{
    public GameObject newPrefab; // The pizza with cheese prefab
    private HashSet<GameObject> processedObjects = new HashSet<GameObject>(); // Track processed objects

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object is a PizzaSauce and hasn't been processed yet
        if (other.CompareTag("PizzaSauce") && !processedObjects.Contains(other.gameObject))
        {
            processedObjects.Add(other.gameObject); // Mark the object as processed
            ChangeBread(other.gameObject);

            // Destroy the cheese object itself
            Destroy(gameObject);
        }
    }

    void ChangeBread(GameObject bread)
    {
        // Save the position and rotation of the current pizza
        Vector3 position = bread.transform.position;
        Quaternion rotation = bread.transform.rotation;

        // Destroy the current pizza object
        Destroy(bread);

        // Instantiate the new prefab with cheese in its place
        GameObject newBread = Instantiate(newPrefab, position, rotation);

        // Optional: Update the tag of the new prefab to avoid retriggering
        if (newBread.CompareTag("PizzaSauce"))
        {
            newBread.tag = "CheesePizza";
        }
    }
}
