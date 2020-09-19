using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantController : MenuController
{
    public static MerchantController Instance;
    public bool isOpened { get; private set; } = false;

    public Merchant merchant { private get; set; } = null;
    [SerializeField]
    private GameObject playerUI = null;
    [SerializeField]
    private GameObject merchantUI = null;
    [SerializeField]
    private GameObject inventorySlotPrefab = null;
    [SerializeField]
    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        multiplesMenus.Add(this);
        multiplesMenus.Add(InventoryController.Instance);

        Close();
    }

    private void Update()
    {
        if (isActived && merchantUI.activeSelf)
            SelectControllerVerticalAndHorizontal();
    }

    public bool IsActived()
    {
        return merchantUI.activeSelf;
    }

    public void Open(Merchant merchant)
    {
        if (merchant == null || merchant.listItems == null || merchant.listItems.Count == 0)
            return;

        isActived = true;
        this.merchant = merchant;
        PopulateStoreList();

        InventoryController.Instance.OpenMerchant(multiplesMenus);
        ActivateVisual(true);

        GetColumnAndRowInTheEndOfFrame(merchantUI.GetComponent<GridLayoutGroup>());
        GameManager.Instance.Pause();
    }

    public void Close()
    {
        if (merchantUI.gameObject.activeSelf)
        {
            UpdateMerchantList();
            RemoveStoreList();
            ActivateVisual(false);

            DisableAllMultiplesMenus();
            ResetInputController();

            InventoryController.Instance.CloseMerchant();
        }
    }

    private void ActivateVisual(bool enabled)
    {
        isOpened = enabled;
        merchantUI.SetActive(enabled);
        playerUI.SetActive(enabled);
    }

    #region List Controller
    private void PopulateStoreList()
    {
        for (int i = 0; i < merchant.listItems.Count; i++)
        {
            InventorySlot inventorySlot = Instantiate(inventorySlotPrefab, merchantUI.transform).GetComponent<InventorySlot>();
            inventorySlot.AddItem(merchant.listItems[i]);
            inventorySlot.ShowIcon(true);
            menuInGame.Add(inventorySlot.GetComponentInChildren<Button>());
            slots.Add(inventorySlot);
        }
    }

    private void UpdateMerchantList()
    {
        if (merchant != null)
        {
            List<Item> newList = new List<Item>();

            foreach (InventorySlot inventorySlot in merchantUI.GetComponentsInChildren<InventorySlot>())
                if (inventorySlot.item != null)
                    newList.Add(inventorySlot.item);

            merchant.listItems = newList;
            merchant = null;
        }
    }

    private void RemoveStoreList()
    {
        foreach (Transform child in merchantUI.transform)
            Destroy(child.gameObject);

        slots.Clear();
        menuInGame.Clear();
    }
    #endregion
}
