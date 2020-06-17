using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MenuController
{
    public static InventoryManager Instance;

    [Header("Inventory Manager")]
    [SerializeField]
    private GameObject visual = null;
    private RectTransform rectTransform = null;
    [SerializeField]
    private GameObject inventorySlotPrefab = null;
    [SerializeField]
    private GameObject anchorToVisualMerchant = null;
    [SerializeField]
    private InventorySlot[] slots = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        rectTransform = visual.GetComponent<RectTransform>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(inventorySlotPrefab, visual.transform).GetComponent<InventorySlot>();
            slots[i].ShowIcon(false);
            menuInGame.Add(slots[i].GetComponentInChildren<Button>());
        }

        Init();
    }

    private IEnumerator GetColumnAndRowInTheEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        GridLayoutGroupUtil.GetColumnAndRow(visual.GetComponent<GridLayoutGroup>(), out column, out row);
    }

    private void Update()
    {
        if (visual.activeSelf)
            base.SelectControllerVerticalAndHorizontal(column, row);
    }

    public void ActiveMenu(bool enabled)
    {
        ResetRectTransform();
        visual.SetActive(enabled);
        StartCoroutine(GetColumnAndRowInTheEndOfFrame());
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

    public bool PickUpItem(Item item)
    {
        bool canTake = false;

        foreach (InventorySlot slot in slots)
        {
            if (slot != null && slot.item == null)
            {
                slot.AddItem(item);
                canTake = true;
                break;
            }
        }

        return canTake;
    }

    public void OpenMerchant(MenuController[] multiplesMenus)
    {
        isActived = false;
        this.multiplesMenus = multiplesMenus;
        visual.transform.SetParent(anchorToVisualMerchant.transform);
        ActiveMenu(true);
    }

    public void CloseMerchant()
    {
        isActived = true;
        multiplesMenus = null;
        visual.transform.SetParent(transform);
        ActiveMenu(false);
    }
}
