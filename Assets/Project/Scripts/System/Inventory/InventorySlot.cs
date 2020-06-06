using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item { get; set; } = null;
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
            removeButton.SetActive(true);
        }
        else
        {
            currentAlpha = 0;
            removeButton.SetActive(false);
        }

        tempColor.a = currentAlpha;
        icon.color = tempColor;
    }

    public void RemoveItem()
    {
        ShowIcon(false);
        item = null;
        icon.sprite = null;
    }
}
