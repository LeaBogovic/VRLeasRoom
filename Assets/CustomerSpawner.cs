using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    public void SpawnCustomer(string orderText)
    {
        GameObject customer = Instantiate(customerPrefab, spawnPoint.position, spawnPoint.rotation);
        CustomerOrder customerOrder = customer.GetComponent<CustomerOrder>();
        customerOrder.orderText = orderText; // Set the order text
    }
}
