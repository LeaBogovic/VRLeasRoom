using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaMushroom : MonoBehaviour
{
    public GameObject newPrefab; // Prefab for the mushrooms
    private HashSet<GameObject> processedObjects = new HashSet<GameObject>(); // Track processed objects

    private void OnTriggerEnter(Collider other)
    {
        // Check if the pizza is tagged as PepperoniPizza and hasn't already been processed
        if (other.CompareTag("PEPPERONIPIZZA") && !processedObjects.Contains(other.gameObject))
        {
            processedObjects.Add(other.gameObject); // Mark as processed
            AddMushrooms(other.gameObject);
        }
    }

    void AddMushrooms(GameObject pizza)
    {
        // Preserve the position and rotation of the pizza
        Vector3 pizzaPosition = pizza.transform.position;
        Quaternion pizzaRotation = pizza.transform.rotation;

        // Destroy the pizza to apply the new one
        Destroy(pizza);

        // Instantiate the new pizza with mushrooms
        GameObject newPizza = Instantiate(newPrefab, pizzaPosition, pizzaRotation);

        // Change the tag to reflect the mushrooms have been added
        if (newPizza.CompareTag("PEPPERONIPIZZA"))
        {
            newPizza.tag = "MUSHROOMANDPEPERONNIPIZZA"; // Update the tag for pepperoni pizza with mushrooms
        }

        // Destroy the mushroom object after it has applied to the pizza
        Destroy(gameObject);
    }
}


