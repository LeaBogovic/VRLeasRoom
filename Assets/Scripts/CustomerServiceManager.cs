using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerServiceManager : MonoBehaviour
{
    public Light indicatorLight; // The light to check if it's green
    public GameObject currentCustomer; // The current customer GameObject
    public GameObject currentPizza; // The current pizza GameObject
    public GameObject customerPrefab; // Prefab for a new customer
    public GameObject pizzaPrefab; // Prefab for a new pizza
    public Transform customerSpawnPoint; // Where the new customer should spawn
    public Transform pizzaSpawnPoint; // Where the new pizza should spawn

    public void OnButtonPress()
    {
        if (indicatorLight != null && indicatorLight.color == Color.green)
        {
            // Light is green; remove current customer and pizza
            if (currentCustomer != null)
            {
                Destroy(currentCustomer);
            }

            if (currentPizza != null)
            {
                Destroy(currentPizza);
            }

            // Spawn a new customer
            if (customerPrefab != null && customerSpawnPoint != null)
            {
                currentCustomer = Instantiate(customerPrefab, customerSpawnPoint.position, customerSpawnPoint.rotation);
            }

            // Spawn a new pizza
            if (pizzaPrefab != null && pizzaSpawnPoint != null)
            {
                currentPizza = Instantiate(pizzaPrefab, pizzaSpawnPoint.position, pizzaSpawnPoint.rotation);
            }
        }
        else
        {
            Debug.Log("Action not allowed: The light is not green.");
        }
    }
}
