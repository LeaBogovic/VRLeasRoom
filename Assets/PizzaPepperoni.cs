using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPepperoni : MonoBehaviour
{
    public GameObject newPrefab; // Prefab for the pepperoni pizza
    private HashSet<GameObject> processedObjects = new HashSet<GameObject>(); // Track processed objects

    private void OnTriggerEnter(Collider other)
    {
        // Check if the pizza is tagged as CheesePizza and hasn't already been processed
        if (other.CompareTag("CheesePizza") && !processedObjects.Contains(other.gameObject))
        {
            processedObjects.Add(other.gameObject); // Mark as processed
            AddPepperoni(other.gameObject);
        }
    }

    void AddPepperoni(GameObject pizza)
    {
        // Preserve the position and rotation of the pizza
        Vector3 pizzaPosition = pizza.transform.position;
        Quaternion pizzaRotation = pizza.transform.rotation;

        // Destroy the original pizza to apply the new one
        Destroy(pizza);

        // Instantiate the new pizza with pepperoni
        GameObject newPizza = Instantiate(newPrefab, pizzaPosition, pizzaRotation);

        // Change the tag to reflect the pepperoni has been added
        if (newPizza.CompareTag("CheesePizza"))
        {
            newPizza.tag = "PEPPERONIPIZZA"; // Update the tag for the cheese pizza with pepperoni
        }

        // Destroy the pepperoni object after it has applied to the pizza
        Destroy(gameObject);
    }
}
