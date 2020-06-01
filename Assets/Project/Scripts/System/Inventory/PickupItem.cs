using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickupItem : MonoBehaviour
{
    private InventoryManager inventoryManager = null;

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            inventoryManager.PickUpItem();
    }
}
