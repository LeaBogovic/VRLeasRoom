using UnityEngine;
using TMPro; // For TextMeshPro

public class SpawnCustomerOnButtonPress : MonoBehaviour
{
    public GameObject currentCustomer; // The current customer in the scene
    public GameObject customerPrefab; // The prefab for new customers
    public Transform spawnPoint; // Spawn point for the new customer
    public TextMeshPro statusText; // 3D text component for the status message
    public PizzaLightIndicator pizzaLightIndicator; // Reference to the PizzaLightIndicator script
    public Collider trayCollider; // A collider with trigger enabled to detect objects inside

    private void Start()
    {
        if (pizzaLightIndicator == null)
        {
            Debug.LogError("PizzaLightIndicator is not assigned in the inspector.");
        }

        if (trayCollider == null)
        {
            Debug.LogError("Tray Collider is not assigned in the inspector.");
        }
        else if (!trayCollider.isTrigger)
        {
            Debug.LogError("Tray Collider must have 'Is Trigger' enabled.");
        }
    }

    // This method is called when the VR button is pressed
    public void OnButtonPress()
    {
        if (pizzaLightIndicator != null && pizzaLightIndicator.IsLightGreen())
        {
            DestroyObjectsInTray();    // Destroy all objects inside the tray collider
            DespawnCurrentCustomer(); // Deactivate or destroy the current customer
            SpawnCustomer();          // Spawn a new customer
            ChangeText();             // Update the status text
        }
        else
        {
            Debug.Log("Cannot spawn customer. The light is not green.");
        }
    }

    private void DestroyObjectsInTray()
    {
        if (trayCollider != null)
        {
            // Get all objects currently overlapping the collider
            Collider[] objectsInTray = Physics.OverlapBox(
                trayCollider.bounds.center,
                trayCollider.bounds.extents,
                trayCollider.transform.rotation
            );

            foreach (var obj in objectsInTray)
            {
                if (obj.gameObject != trayCollider.gameObject) // Avoid destroying the tray itself
                {
                    Debug.Log($"Destroying object: {obj.gameObject.name}");
                    Destroy(obj.gameObject);
                }
            }
        }
    }

    private void SpawnCustomer()
    {
        if (customerPrefab != null && spawnPoint != null)
        {
            currentCustomer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("New customer spawned.");
        }
        else
        {
            Debug.LogError("Customer prefab or spawn point is not assigned.");
        }
    }

    private void DespawnCurrentCustomer()
    {
        if (currentCustomer != null)
        {
            Destroy(currentCustomer); // Completely remove the current customer
            Debug.Log("Current customer destroyed.");
        }
        else
        {
            Debug.Log("No current customer to despawn.");
        }
    }

    private void ChangeText()
    {
        if (statusText != null)
        {
            statusText.text = "One Pepperoni Pizza"; // Update the text
            Debug.Log("Status text updated to: One Pepperoni Pizza.");
        }
        else
        {
            Debug.LogError("TextMeshPro component is not assigned.");
        }
    }
}
