[System.Serializable]
public class PlayerData
{
    public int gold = 0;
    public float[] position = null;
    public InventoryData inventory = null;

    public PlayerData(PlayerManager player)
    {
        gold = player.gold;

        GetPosition(player);
        GetInventory(player.inventory);
    }

    private void GetPosition(PlayerManager player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

    private void GetInventory(InventoryObject inventory)
    {
        this.inventory = new InventoryData(inventory.slots);
    }
}
