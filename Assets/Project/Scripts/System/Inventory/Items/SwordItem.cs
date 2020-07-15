using UnityEngine;

[CreateAssetMenu(fileName = "Sword", menuName = "Item/Sword")]
public class SwordItem : Item
{
    public int attackForce;

    public override ItemTag itemTag { get; set; } = ItemTag.Sword;

    public override void Use()
    {
        InventoryController.Instance.EquipSword(this);
    }
}
