using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Default")]
public class Item : ScriptableObject
{
    new public string name = "Item Name";
    public Sprite icon = null;
    public int slotLimit = 1;
    public int salePrice = 1;
    public int purchasePrice = 1;

    public virtual void Use() { }
}
