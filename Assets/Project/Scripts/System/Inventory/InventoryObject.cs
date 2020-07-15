using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    [Header("Inventory Manager")]
    public InventorySlot[] slots = null;

    [Header("Equipments")]
    private SwordItem sword = null;

    public bool PickUpItem(Item item)
    {
        bool canTake = FindItemInListAndAdd(item);

        if (!canTake)
            canTake = FindAndAddEmptySlotItem(item);

        return canTake;
    }

    #region Equipments
    public void EquipSword(SwordItem newSword)
    {
        sword = newSword;
    }

    public int GetSwordBonus()
    {
        if (sword == null)
            return 0;
        else
            return sword.attackForce;
    }
    #endregion

    #region Find
    private void FindItemInListAndRemove(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.item = null;
            }
        }
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
    #endregion

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
