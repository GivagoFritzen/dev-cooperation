using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Merchant : MonoBehaviour
{
    [SerializeField]
    private new string name = "";
    public List<Item> listItems = new List<Item>();

    private void Start()
    {
        listItems.RemoveAll(item => item == null);
    }

    #region Get
    public string GetName()
    {
        return name;
    }

    public ItemData[] GetItemsData()
    {
        ItemData[] itemsData = new ItemData[listItems.Count];

        for (int index = 0; index < listItems.Count; index++)
            if (listItems[index] != null)
                itemsData[index] = new ItemData(listItems[index], new float[3] { 0, 0, 0 });

        return itemsData;
    }
    #endregion

    #region Set
    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetItemsDataToListItems(ItemData[] newListItems)
    {
        for (int index = 0; index < newListItems.Length; index++)
        {
            if (newListItems[index] != null)
            {
                Item newItem = (Item)Resources.Load(RouteUtil.GetPrefabsItems() + StringUtil.RemoveWhitespace(newListItems[index].name), typeof(Item)); ;
                listItems.Add(newItem);
            }
        }
    }
    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && InputManager.Instance.GetAction())
            MerchantController.Instance.Open(this);
    }
}
