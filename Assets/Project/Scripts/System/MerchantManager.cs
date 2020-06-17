using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MerchantManager : MenuController
{
    public static MerchantManager Instance;
    public bool isOpened { get; private set; } = false;

    [Header("Merchant Manager")]
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
        Close();
    }

    private void Update()
    {
        if (isActived && merchantUI.activeSelf)
            base.SelectControllerVerticalAndHorizontal(column, row);
    }

    private IEnumerator GetColumnAndRowInTheEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        GridLayoutGroupUtil.GetColumnAndRow(merchantUI.GetComponent<GridLayoutGroup>(), out column, out row);
    }

    public void Open(Item[] listItems)
    {
        if (listItems == null || listItems.Length == 0)
            return;

        isActived = true;
        this.listItems = listItems;
        PopulateStoreList();
        InventoryManager.Instance.OpenMerchant(multiplesMenus);
        ActivateVisual(true);
        StartCoroutine(GetColumnAndRowInTheEndOfFrame());
        MenuManager.Instance.Pause();
    }

    public Item[] Close()
    {
        InventoryManager.Instance.CloseMerchant();
        RemoveStoreList();
        ActivateVisual(false);
        DisableAllMultiplesMenus();
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
