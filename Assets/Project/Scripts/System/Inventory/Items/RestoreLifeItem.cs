using UnityEngine;

[CreateAssetMenu(fileName = "RestoreLifeItem", menuName = "RestoreLifeItem")]
public class RestoreLifeItem : Item
{
    public int restoreLife;

    public override void Use()
    {
        PlayerManager.Instance.RestoreLife(restoreLife);
    }
}
