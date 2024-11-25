using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakePizza : MonoBehaviour
{
    public GameObject bakedPizzaPrefab; // The baked version of the pizza

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a pizza-related tag
        if (other.tag.Contains("Pizza"))
        {
            BakeThePizza(other.gameObject);
        }
    }

    void BakeThePizza(GameObject pizza)
    {
        // Save the position and rotation of the pizza
        Vector3 position = pizza.transform.position;
        Quaternion rotation = pizza.transform.rotation;

        // Destroy the original pizza
        Destroy(pizza);

        // Instantiate the baked pizza in its place
        Instantiate(bakedPizzaPrefab, position, rotation);
    }
}
