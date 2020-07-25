using UnityEngine;

[CreateAssetMenu(fileName = "Helmet", menuName = "Item/Helmet")]
public class HelmetItem : Item
{
    public override ItemTag itemTag { get; set; } = ItemTag.Helmet;

    public override void Use()
    {
        InventoryController.Instance.EquipHelmet(this);
    }
}
