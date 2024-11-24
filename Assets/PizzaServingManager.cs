using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaServingManager : MonoBehaviour
{
    public PizzaLightIndicator pizzaLightIndicator; // Reference to the PizzaLightIndicator script
    public GameObject currentCustomer; // The current customer in the scene
    public GameObject nextCustomer; // The next customer to activate after serving
    public GameObject tray; // Reference to the tray object where pizzas are placed

    public void OnButtonPress()
    {
        if (pizzaLightIndicator != null && pizzaLightIndicator.indicatorLight.color == Color.green)
        {
            // If the light is green, serve the pizza
            GameObject pizzaOnTray = FindPizzaOnTray();

            if (pizzaOnTray != null)
            {
                Destroy(pizzaOnTray); // Remove the pizza
            }

            if (currentCustomer != null)
            {
                Destroy(currentCustomer); // Remove the current customer
            }

            if (nextCustomer != null)
            {
                nextCustomer.SetActive(true); // Activate the next customer
            }

            // Reset the tray light
            pizzaLightIndicator.indicatorLight.color = Color.white;
        }
        else
        {
            Debug.Log("Cannot serve: Light is not green or no pizza on tray.");
        }
    }

    private GameObject FindPizzaOnTray()
    {
        // Searches for a pizza GameObject near the tray based on proximity
        Collider[] hitColliders = Physics.OverlapSphere(tray.transform.position, 0.5f); // Adjust radius as needed
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("CookedCheesePizza") || collider.CompareTag("OtherPizzaTags")) // Add other tags if needed
            {
                return collider.gameObject;
            }
        }
        return null;
    }
}
