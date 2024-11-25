using UnityEngine;

public class SausageChanger : MonoBehaviour
{
    public GameObject newPrefab; // The new prefab to replace the Sausage
    private float timer = 0f; // Timer to track how long the Sausage has been in the trigger
    private bool isSausageInside = false; // Flag to check if the Sausage is inside the collider
    private GameObject currentSausage; // Reference to the current Sausage object

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the collider is tagged as "Sausage"
        if (other.CompareTag("Sausage"))
        {
            isSausageInside = true;
            currentSausage = other.gameObject;
            timer = 0f; // Reset the timer when the Sausage enters
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the collider is the Sausage
        if (other.CompareTag("Sausage"))
        {
            isSausageInside = false;
            timer = 0f; // Reset the timer if the Sausage exits
            currentSausage = null; // Clear the reference to the Sausage
        }
    }

    private void Update()
    {
        // If a Sausage is inside the trigger, start counting time
        if (isSausageInside)
        {
            timer += Time.deltaTime; // Increase the timer

            if (timer >= 5f) // After 5 seconds
            {
                ChangeToNewPrefab();
                isSausageInside = false; // Stop counting after replacement
            }
        }
    }

    void ChangeToNewPrefab()
    {
        if (currentSausage != null)
        {
            // Replace the Sausage with the new prefab at the same position and rotation
            Instantiate(newPrefab, currentSausage.transform.position, currentSausage.transform.rotation);

            // Destroy the old Sausage object
            Destroy(currentSausage);
        }
    }
}
