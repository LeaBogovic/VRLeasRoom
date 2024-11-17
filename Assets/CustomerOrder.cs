using UnityEngine;

public class CustomerOrder : MonoBehaviour
{
    public GameObject orderUI; // Assign the UI prefab for displaying orders
    public string orderText; // Preset order (e.g., "Pepperoni Pizza")

    void Start()
    {
        // Display the order
        if (orderUI != null)
        {
            orderUI.GetComponentInChildren<TextMesh>().text = orderText;
        }
    }
}
