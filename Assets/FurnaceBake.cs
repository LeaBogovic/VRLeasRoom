using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceBake : MonoBehaviour
{
    public Transform bakePosition; // Position where baked pizza will appear
    public float maxBakeTime = 10f; // Maximum time before pizza becomes burned

    [System.Serializable]
    public class PizzaBakeData
    {
        public string tag; // The tag of the pizza to detect
        public GameObject bakedPrefab; // The baked version prefab
        public GameObject burnedPrefab; // The burned version prefab
        public float bakeTime; // Time it takes to bake this pizza
    }

    public List<PizzaBakeData> pizzaBakeSettings = new List<PizzaBakeData>(); // Settings for each pizza type

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the furnace is a pizza
        foreach (var pizzaData in pizzaBakeSettings)
        {
            if (other.CompareTag(pizzaData.tag))
            {
                // Start baking process for the pizza
                StartCoroutine(BakePizza(other.gameObject, pizzaData));
                break;
            }
        }
    }

    private IEnumerator BakePizza(GameObject pizza, PizzaBakeData pizzaData)
    {
        // Check if pizza is already burned or baked (prevent transformation)
        if (pizza.CompareTag("BurnedPizza") || pizza.CompareTag("Pizza"))
            yield break;

        float timeBaked = 0f;

        // Wait for the bake time, but also check for the maximum allowed time
        while (timeBaked < pizzaData.bakeTime && timeBaked < maxBakeTime)
        {
            timeBaked += Time.deltaTime;

            // Debugging log to check the current bake time
            Debug.Log("Baking time: " + timeBaked + " seconds.");

            yield return null;
        }

        // Check if pizza is done baking and change it to the baked prefab
        if (timeBaked >= pizzaData.bakeTime && !pizza.CompareTag("Pizza"))
        {
            Debug.Log("Pizza is baked, transforming.");
            TransformPizza(pizza, pizzaData.bakedPrefab, "Pizza");
        }

        // If the pizza has been in the oven for longer than the max allowed time, turn it into a burned pizza
        if (timeBaked >= maxBakeTime)
        {
            Debug.Log("Pizza is burned, transforming.");
            TransformPizza(pizza, pizzaData.burnedPrefab, "BurnedPizza");
        }
    }

    void TransformPizza(GameObject pizza, GameObject pizzaPrefab, string tag)
    {
        // Prevent multiple instantiations by marking the pizza as "baked" or "burned"
        pizza.tag = tag; // Update the tag to reflect baked or burned status

        Vector3 position = bakePosition != null ? bakePosition.position : pizza.transform.position;
        Quaternion rotation = bakePosition != null ? bakePosition.rotation : pizza.transform.rotation;

        // Destroy the old pizza and replace it with the new one (baked or burned)
        Destroy(pizza); // Remove the original pizza
        GameObject newPizza = Instantiate(pizzaPrefab, position, rotation); // Spawn the baked or burned pizza

        // Debugging the transformation
        Debug.Log("Pizza transformed into " + pizzaPrefab.name);

        // Optional: If the new pizza has a collider, you might want to re-enable it if needed
        newPizza.GetComponent<Collider>().enabled = true;
    }
}
