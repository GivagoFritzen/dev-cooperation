[System.Serializable]
public class InventoryData
{
    public string[] items = null;
    public int[] amounts = null;

    public InventoryData(InventorySlot[] inventorySlots)
    {
        items = new string[inventorySlots.Length];
        amounts = new int[inventorySlots.Length];

        for (int index = 0; index < inventorySlots.Length; index++)
        {
            items[index] = inventorySlots[index].item ? inventorySlots[index].item.name : "";
            amounts[index] = inventorySlots[index].amount;
        }
    }
}
