using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Item/Armor")]
public class ArmorItem : Item
{
    public override ItemTag itemTag { get; set; } = ItemTag.Armor;

    public override void Use()
    {
        InventoryController.Instance.EquipArmor(this);
    }
}

