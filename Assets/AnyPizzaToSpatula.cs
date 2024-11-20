using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyPizzaToSpatula : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a tag containing "Pizza"
        if (other.tag.Contains("Pizza"))
        {
            AttachPizzaToSpatula(other.gameObject);
        }
    }

    void AttachPizzaToSpatula(GameObject pizza)
    {
        // Move the pizza to the spatula's position
        pizza.transform.position = transform.position;

        // Make the pizza a child of the spatula
        pizza.transform.SetParent(transform);

        // Optionally, reset rotation or adjust offsets to align it perfectly
        pizza.transform.rotation = transform.rotation;
    }
}
