using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantController : MenuController
{
    public static MerchantController Instance;
    public bool isOpened { get; private set; } = false;

    private Item[] listItems = null;
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

    public void Open(Item[] listItems)
    {
        if (listItems == null || listItems.Length == 0)
            return;

        isActived = true;
        this.listItems = listItems;
        PopulateStoreList();
        InventoryController.Instance.OpenMerchant(multiplesMenus);
        ActivateVisual(true);
        GetColumnAndRowInTheEndOfFrame(merchantUI.GetComponent<GridLayoutGroup>());
        MenuManager.Instance.Pause();
    }

    public Item[] Close()
    {
        if (!merchantUI.gameObject.activeSelf)
            return null;

        RemoveStoreList();
        ActivateVisual(false);
        DisableAllMultiplesMenus();
        InventoryController.Instance.CloseMerchant();
        ResetInputController();
        return listItems;
    }

    private void PopulateStoreList()
    {
        for (int i = 0; i < listItems.Length; i++)
        {
            InventorySlot inventorySlot = Instantiate(inventorySlotPrefab, merchantUI.transform).GetComponent<InventorySlot>();
            inventorySlot.AddItem(listItems[i]);
            inventorySlot.ShowIcon(true);
            menuInGame.Add(inventorySlot.GetComponentInChildren<Button>());
            slots.Add(inventorySlot);
        }
    }

    private void RemoveStoreList()
    {
        foreach (Transform child in merchantUI.transform)
            Destroy(child.gameObject);

        slots.Clear();
        menuInGame.Clear();
    }

    private void ActivateVisual(bool enabled)
    {
        isOpened = enabled;
        merchantUI.SetActive(enabled);
        playerUI.SetActive(enabled);
    }
}
