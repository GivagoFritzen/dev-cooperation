using UnityEngine;

[CreateAssetMenu(fileName = "RestoreLifeItem", menuName = "Item/Restore Life Item")]
public class RestoreLifeItem : Item
{
    public int restoreLife;

    public override void Use()
    {
        PlayerManager.Instance.RestoreLife(restoreLife);
    }
}
