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

    private Dictionary<GameObject, Coroutine> bakingPizzas = new Dictionary<GameObject, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the furnace is a pizza
        foreach (var pizzaData in pizzaBakeSettings)
        {
            if (other.CompareTag(pizzaData.tag))
            {
                // Start baking process for the pizza if not already baking
                if (!bakingPizzas.ContainsKey(other.gameObject))
                {
                    Coroutine bakingCoroutine = StartCoroutine(BakePizza(other.gameObject, pizzaData));
                    bakingPizzas[other.gameObject] = bakingCoroutine;
                }
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Stop the baking process if the pizza leaves the oven
        if (bakingPizzas.ContainsKey(other.gameObject))
        {
            StopCoroutine(bakingPizzas[other.gameObject]);
            bakingPizzas.Remove(other.gameObject);
            Debug.Log("Pizza removed from oven, baking stopped.");
        }
    }

    private IEnumerator BakePizza(GameObject pizza, PizzaBakeData pizzaData)
    {
        float timeBaked = 0f;

        // Wait for the bake time or max bake time while pizza is still inside the collider
        while (timeBaked < maxBakeTime)
        {
            timeBaked += Time.deltaTime;

            // Debugging log to check the current bake time
            Debug.Log($"Baking {pizza.name}: {timeBaked}/{pizzaData.bakeTime} seconds.");

            if (timeBaked >= pizzaData.bakeTime)
            {
                Debug.Log("Pizza is baked, transforming.");
                TransformPizza(pizza, pizzaData.bakedPrefab, "Pizza");
                bakingPizzas.Remove(pizza);
                yield break;
            }

            yield return null;
        }

        // If the pizza has been in the oven for longer than the max allowed time, turn it into a burned pizza
        Debug.Log("Pizza is burned, transforming.");
        TransformPizza(pizza, pizzaData.burnedPrefab, "BurnedPizza");
        bakingPizzas.Remove(pizza);
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
        Debug.Log($"Pizza transformed into {pizzaPrefab.name}");
    }
}
