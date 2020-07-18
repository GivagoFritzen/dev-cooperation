using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Item item { get; set; } = null;
    public int amount = 0;
    [SerializeField]
    public bool sellerMerchandise = false;
    [SerializeField]
    private Image icon = null;
    [SerializeField]
    private GameObject removeButton = null;
    private PlayerManager player = null;
    [SerializeField]
    private GameObject visualAmount = null;
    [SerializeField]
    private TextMeshProUGUI amountText = null;

    private void Start()
    {
        player = PlayerManager.Instance;
        ShowAmount();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
        amount += 1;
        SetIcon();
        ShowIcon(true);
        ShowAmount();
    }

    public void SetIcon()
    {
        icon.sprite = item.icon;
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

    public void ShowAmount()
    {
        if (amountText == null)
            return;

        if (amount > 1)
        {
            visualAmount.SetActive(true);
            amountText.text = amount.ToString();
        }
        else
        {
            visualAmount.SetActive(false);
        }
    }

    public void UseItem()
    {
        if (item == null || amount <= 0)
            return;

        if (MerchantController.Instance.IsActived())
        {
            SellItem();
        }
        else if (IsEquipment())
        {
            SwordController();
        }
        else
        {
            item.Use();
            ReduceAmount();
        }
    }

    #region Equipments
    private bool IsEquipment()
    {
        return item.itemTag == ItemTag.Sword || item.itemTag == ItemTag.Armor || item.itemTag == ItemTag.Shield || item.itemTag == ItemTag.Helmet;
    }

    private void SwordController()
    {
        if (item.itemTag != ItemTag.Sword)
            return;

        Item currentSword = InventoryController.Instance.sword.item;
        item.Use();

        if (currentSword == null)
        {
            item = null;
            ShowIcon(false);
        }
        else
        {
            item = currentSword;
            SetIcon();
            ShowIcon(true);
        }
    }
    #endregion

    public void SellItem()
    {
        if (item == null || amount <= 0)
            return;

        if (sellerMerchandise && player.gold >= item.purchasePrice)
        {
            bool hasSpaceInTheInventory = player.inventory.PickUpItem(item);
            if (hasSpaceInTheInventory)
            {
                player.gold -= item.purchasePrice;
                PlayerManager.Instance.UpdateMoneyUI();

                ReduceAmount();
            }
        }
        else if (!sellerMerchandise)
        {
            player.gold += item.salePrice;
            PlayerManager.Instance.UpdateMoneyUI();

            ReduceAmount();
            ShowAmount();
        }
    }

    private void ReduceAmount()
    {
        amount -= 1;
        if (amount < 1)
            RemoveItem();
    }

    public void RemoveItem()
    {
        ShowIcon(false);
        ShowAmount();
        item = null;
        icon.sprite = null;
    }
}
