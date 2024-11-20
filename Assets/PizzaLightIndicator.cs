using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaLightIndicator : MonoBehaviour
{
    public Light indicatorLight; // Assign your light in the inspector
    private bool isPizzaOnCollider = false; // To track if there's a pizza on the collider

    private void Start()
    {
        // Ensure the light starts white
        if (indicatorLight != null)
        {
            indicatorLight.color = Color.white;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CookedCheesePizza"))
        {
            // Object is a cooked cheese pizza
            isPizzaOnCollider = true;
            SetLightColor(Color.green);
        }
        else
        {
            // Object is not a cooked cheese pizza
            isPizzaOnCollider = true;
            SetLightColor(Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset to white when the object leaves the collider
        isPizzaOnCollider = false;
        SetLightColor(Color.white);
    }

    private void SetLightColor(Color color)
    {
        if (indicatorLight != null)
        {
            indicatorLight.color = color;
        }
    }
}
