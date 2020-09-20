using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Item item { get; set; } = null;
    public int amount = 0;

    [SerializeField]
    private Image icon = null;
    [SerializeField]
    private GameObject removeButton = null;

    [SerializeField]
    private GameObject visualAmount = null;
    [SerializeField]
    private TextMeshProUGUI amountText = null;

    public bool sellerMerchandise = false;
    private PlayerManager player = null;

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
            ArmorController();
            ShieldController();
            HelmetController();
        }
        else
        {
            item.Use();
            ReduceAmount();
        }
    }

    #region Sell/Remove Item
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
        if (item.itemTag == ItemTag.Book)
            return;

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
    #endregion

    #region Show Controller
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
    #endregion

    #region Equipments
    private bool IsEquipment()
    {
        return item.itemTag == ItemTag.Sword || item.itemTag == ItemTag.Armor || item.itemTag == ItemTag.Shield || item.itemTag == ItemTag.Helmet;
    }

    private void SwordController()
    {
        if (item == null || item.itemTag != ItemTag.Sword)
            return;

        Item currentSword = InventoryController.Instance.sword.item;
        Equip(currentSword);
    }

    public void UnequipSword()
    {
        if (InventoryController.Instance.sword.item == null)
            return;

        bool keep = PlayerManager.Instance.inventory.PickUpItem(item);
        if (keep)
        {
            InventoryController.Instance.sword.item = null;
            ReduceAmount();
        }
    }

    private void ArmorController()
    {
        if (item == null || item.itemTag != ItemTag.Armor)
            return;

        Item currentArmor = InventoryController.Instance.armor.item;
        Equip(currentArmor);
    }

    public void UnequipArmor()
    {
        if (InventoryController.Instance.armor.item == null)
            return;

        bool keep = PlayerManager.Instance.inventory.PickUpItem(item);
        if (keep)
        {
            InventoryController.Instance.armor.item = null;
            ReduceAmount();
        }
    }

    private void ShieldController()
    {
        if (item == null || item.itemTag != ItemTag.Shield)
            return;

        Item currentShield = InventoryController.Instance.shield.item;
        Equip(currentShield);
    }

    public void UnequipShield()
    {
        if (InventoryController.Instance.shield.item == null)
            return;

        bool keep = PlayerManager.Instance.inventory.PickUpItem(item);
        if (keep)
        {
            InventoryController.Instance.shield.item = null;
            ReduceAmount();
        }
    }

    private void HelmetController()
    {
        if (item == null || item.itemTag != ItemTag.Helmet)
            return;

        Item currentHelmet = InventoryController.Instance.helmet.item;
        Equip(currentHelmet);
    }

    public void UnequipHelmet()
    {
        if (InventoryController.Instance.helmet.item == null)
            return;

        bool keep = PlayerManager.Instance.inventory.PickUpItem(item);
        if (keep)
        {
            InventoryController.Instance.helmet.item = null;
            ReduceAmount();
        }
    }

    private void Equip(Item equip)
    {
        item.Use();
        ReduceAmount();

        if (equip == null)
        {
            item = null;
            ShowIcon(false);
        }
        else
        {
            item = equip;
            SetIcon();
            ShowIcon(true);
        }
    }
    #endregion
}
