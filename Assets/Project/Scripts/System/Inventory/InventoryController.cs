using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MenuController
{
    public static InventoryController Instance;

    [SerializeField]
    private GameObject visual = null;
    private RectTransform rectTransform = null;
    [SerializeField]
    private GameObject inventorySlotPrefab = null;
    [SerializeField]
    private GameObject anchorToVisualMerchant = null;
    [SerializeField]
    private InventoryObject inventoryObject = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        rectTransform = visual.GetComponent<RectTransform>();

        for (int i = 0; i < inventoryObject.slots.Length; i++)
        {
            inventoryObject.slots[i] = Instantiate(inventorySlotPrefab, visual.transform).GetComponent<InventorySlot>();
            inventoryObject.slots[i].ShowIcon(false);
            menuInGame.Add(inventoryObject.slots[i].GetComponentInChildren<Button>());
        }

        Init();
    }

    private void Update()
    {
        if (isActived && visual.activeSelf)
            SelectControllerVerticalAndHorizontal();
    }

    public void ActiveMenu(bool enabled)
    {
        ResetRectTransform();
        visual.SetActive(enabled);
        GetColumnAndRowInTheEndOfFrame(visual.GetComponent<GridLayoutGroup>());
    }

    private void ResetRectTransform()
    {
        if (rectTransform != null)
        {
            rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
            rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
            rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0);
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0);
        }
    }

    public void OpenMerchant(List<MenuController> multiplesMenus)
    {
        isActived = false;
        this.multiplesMenus = multiplesMenus;
        visual.transform.SetParent(anchorToVisualMerchant.transform);
        DisableSpriteController();
        ActiveMenu(true);
    }

    public void CloseMerchant()
    {
        isActived = true;
        multiplesMenus = new List<MenuController>();
        visual.transform.SetParent(transform);
        ActiveMenu(false);
    }
}
