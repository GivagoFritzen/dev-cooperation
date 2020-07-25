using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Item/Shield")]
public class ShieldItem : Item
{
    public override ItemTag itemTag { get; set; } = ItemTag.Shield;

    public override void Use()
    {
        InventoryController.Instance.EquipShield(this);
    }
}
