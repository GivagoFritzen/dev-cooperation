using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [Header("Inventory Manager")]
    public InventorySlot[] slots = null;

    public bool PickUpItem(Item item)
    {
        bool canTake = FindItemInListAndAdd(item);

        if (!canTake)
            canTake = FindAndAddEmptySlotItem(item);

        return canTake;
    }

    private bool FindItemInListAndAdd(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item && slot.amount < slot.item.slotLimit)
            {
                slot.amount += 1;
                slot.ShowAmount();
                return true;
            }
        }

        return false;
    }

    private bool FindAndAddEmptySlotItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot != null && slot.item == null)
            {
                slot.AddItem(item);
                slot.ShowAmount();
                return true;
            }
        }

        return false;
    }

    public void Load(InventoryData data)
    {
        for (int index = 0; index < data.items.Length; index++)
        {
            slots[index].amount = data.amounts[index];

            if (slots[index].amount <= 0)
            {
                slots[index].item = null;
            }
            else
            {
                string itemName = StringUtil.RemoveWhitespace(data.items[index]);
                slots[index].item = (Item)Resources.Load(RouteUtil.GetPrefabsItems() + itemName, typeof(Item));
                slots[index].SetIcon();
                slots[index].ShowAmount();
                slots[index].ShowIcon(true);
            }
        }
    }
}
