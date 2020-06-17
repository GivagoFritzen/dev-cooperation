using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item { get; set; } = null;
    [SerializeField]
    public bool sellerMerchandise = false;
    [SerializeField]
    private Image icon = null;
    [SerializeField]
    private GameObject removeButton = null;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = newItem.icon;
        ShowIcon(true);
    }

    public void ShowIcon(bool enabled)
    {
        Color tempColor = icon.color;
        float currentAlpha;

        if (enabled)
        {
            currentAlpha = 1;

            if (removeButton != null)
                removeButton.SetActive(true);
        }
        else
        {
            currentAlpha = 0;

            if (removeButton != null)
                removeButton.SetActive(false);
        }

        tempColor.a = currentAlpha;
        icon.color = tempColor;
    }

    public void UseItem()
    {
        if (item == null)
            return;

        item.Use();
        RemoveItem();
    }

    public void SellItem()
    {
        if (sellerMerchandise)
            if (PlayerManager.Instance.gold >= item.purchasePrice)
                PlayerManager.Instance.gold -= item.purchasePrice;
            else
                PlayerManager.Instance.gold += item.salePrice;
    }

    public void RemoveItem()
    {
        ShowIcon(false);
        item = null;
        icon.sprite = null;
    }
}
