using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MenuController
{
    public static InventoryManager Instance;

    [Header("Inventory Manager")]
    [SerializeField]
    private GameObject visual = null;
    [SerializeField]
    private GameObject inventorySlotPrefab = null;
    [SerializeField]
    private InventorySlot[] slots = null;
    private int column = -1;
    private int row = -1;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(inventorySlotPrefab, visual.transform).GetComponent<InventorySlot>();
            slots[i].ShowIcon(false);
            menuInGame.Add(slots[i].GetComponentInChildren<Button>());
        }
    }


    private IEnumerator GetColumnAndRowInTheEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        GridLayoutGroupUtil.GetColumnAndRow(visual.GetComponent<GridLayoutGroup>(), out column, out row);
    }

    private void Update()
    {
        if (visual.activeSelf)
            base.SelectControllerVerticalAndHorizontal(row, column);
    }

    public void ActiveMenu(bool enabled)
    {
        visual.SetActive(enabled);
        StartCoroutine(GetColumnAndRowInTheEndOfFrame());
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
}
