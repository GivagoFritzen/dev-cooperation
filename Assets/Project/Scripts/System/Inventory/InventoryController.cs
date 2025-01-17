﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MenuController
{
    public static InventoryController Instance;

    [Header("Inventory")]
    public GameObject inventoryGameobject = null;
    public GridLayoutGroup gridLayoutGroup { get; set; } = null;
    private RectTransform rectTransform = null;
    [SerializeField]
    private GameObject inventorySlotPrefab = null;
    [SerializeField]
    private GameObject anchorToVisualMerchant = null;
    [SerializeField]
    private InventoryObject inventoryObject = null;

    [Header("Equipments")]
    [SerializeField]
    private GameObject equipmentsGameobject = null;
    public InventorySlot sword = null;
    public InventorySlot armor = null;
    public InventorySlot shield = null;
    public InventorySlot helmet = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        GetComponents();
        PopulateInventorySlots();
        ClearEquipmentsSlots();
        Init();
        SetMultiplesMenus();
    }

    public void GetComponents()
    {
        if (inventoryGameobject == null)
            inventoryGameobject = gameObject.transform.Find("Inventory").gameObject;

        if (gridLayoutGroup == null)
            gridLayoutGroup = inventoryGameobject.GetComponentInChildren<GridLayoutGroup>();
    }

    public void PopulateSlots()
    {
        if (gridLayoutGroup == null)
            gridLayoutGroup = inventoryGameobject.GetComponentInChildren<GridLayoutGroup>();

        PopulateInventorySlots();
    }

    private void PopulateInventorySlots()
    {
        if (rectTransform == null)
        {
            rectTransform = inventoryGameobject.GetComponent<RectTransform>();

            for (int i = 0; i < inventoryObject.slots.Length; i++)
            {
                inventoryObject.slots[i] = Instantiate(inventorySlotPrefab, gridLayoutGroup.transform).GetComponent<InventorySlot>();
                inventoryObject.slots[i].ShowIcon(false);
                menuInGame.Add(inventoryObject.slots[i].GetComponentInChildren<Button>());
            }
        }
    }

    private void Update()
    {
        if (isActived && inventoryGameobject.activeSelf)
            SelectControllerVerticalAndHorizontal();
    }

    public void ActiveMenu(bool enabled, bool activedMerchant = false)
    {
        inventoryGameobject.SetActive(enabled);
        if (!activedMerchant)
            equipmentsGameobject.SetActive(enabled);

        GetColumnAndRowInTheEndOfFrame(gridLayoutGroup);
    }

    private void SetMultiplesMenus()
    {
        multiplesMenus.Add(this);

        MenuController menuControllerEquipments = equipmentsGameobject.GetComponent<MenuController>();
        multiplesMenus.Add(menuControllerEquipments);
        menuControllerEquipments.isActived = false;
    }

    #region Merchant
    public void OpenMerchant(List<MenuController> multiplesMenus)
    {
        isActived = false;
        this.multiplesMenus = multiplesMenus;
        inventoryGameobject.transform.SetParent(anchorToVisualMerchant.transform);
        DisableSpriteController();
        ActiveMenu(true, true);
    }

    public void CloseMerchant()
    {
        isActived = true;
        multiplesMenus = new List<MenuController>();
        inventoryGameobject.transform.SetParent(transform);
        ActiveMenu(false);
        SetMultiplesMenus();
    }
    #endregion

    #region Equipments
    private void ClearEquipmentsSlots()
    {
        sword.RemoveItem();
        armor.RemoveItem();
        shield.RemoveItem();
        helmet.RemoveItem();
    }

    public void EquipSword(SwordItem newSword)
    {
        inventoryObject.EquipSword(newSword);
        sword.AddItem(newSword);
        sword.ShowIcon(true);
    }

    public void EquipArmor(ArmorItem newArmor)
    {
        inventoryObject.EquipArmor(newArmor);
        armor.AddItem(newArmor);
        armor.ShowIcon(true);
    }

    public void EquipShield(ShieldItem newShield)
    {
        inventoryObject.EquipShield(newShield);
        shield.AddItem(newShield);
        shield.ShowIcon(true);
    }

    public void EquipHelmet(HelmetItem newHelmet)
    {
        inventoryObject.EquipHelmet(newHelmet);
        helmet.AddItem(newHelmet);
        helmet.ShowIcon(true);
    }
    #endregion
}
