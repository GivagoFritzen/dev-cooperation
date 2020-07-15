using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Default")]
public abstract class Item : ScriptableObject
{
    new public string name = "Item Name";
    public Sprite icon = null;
    public int slotLimit = 1;
    public int salePrice = 1;
    public int purchasePrice = 1;
    public abstract ItemTag itemTag { get; set; }

    public virtual void Use() { }
}
