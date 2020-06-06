using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField]
    private GameObject visual = null;
    [SerializeField]
    private GameObject inventorySlotPrefab = null;
    [SerializeField]
    private InventorySlot[] slots = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(inventorySlotPrefab, visual.transform).GetComponent<InventorySlot>();
            slots[i].ShowIcon(false);
        }
    }

    public void ActiveMenu(bool enabled)
    {
        visual.SetActive(enabled);
    }

    public bool PickUpItem(Item item)
    {
        bool canTake = false;

        foreach (InventorySlot slot in slots)
        {
            if (slot != null && slot.item == null)
            {
                slot.AddItem(item);
                canTake = true;
                break;
            }
        }

        return canTake;
    }
}
