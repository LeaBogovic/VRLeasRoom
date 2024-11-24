using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaLightIndicator : MonoBehaviour
{
    public Light indicatorLight; // Assign the light in the inspector
    private bool isPizzaOnCollider = false; // Tracks if there's a valid pizza on the collider

    private void Start()
    {
        // Start with the light color as white
        if (indicatorLight != null)
        {
            indicatorLight.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has a valid pizza tag
        if (other.CompareTag("CookedCheesePizza") ||
            other.CompareTag("CookedPepperoniPizza") ||
            other.CompareTag("CookedMushroomPizza"))
        {
            isPizzaOnCollider = true;
            SetLightColor(Color.green); // Set light to green for valid pizzas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the light to white when the object leaves
        if (isPizzaOnCollider)
        {
            isPizzaOnCollider = false;
            SetLightColor(Color.white);
        }
    }

    private void SetLightColor(Color color)
    {
        if (indicatorLight != null)
        {
            indicatorLight.color = color;
        }
    }

    public bool IsLightGreen()
    {
        // Checks if the light is green
        return indicatorLight != null && indicatorLight.color == Color.green;
    }
}
