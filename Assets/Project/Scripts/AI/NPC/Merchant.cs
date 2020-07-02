using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Merchant : MonoBehaviour
{
    [SerializeField]
    private new string name = "";
    public Item[] listItems = null;

    #region Get
    public string GetName()
    {
        return name;
    }

    public ItemData[] GetItemsData()
    {
        ItemData[] itemsData = new ItemData[listItems.Length];

        for (int index = 0; index < listItems.Length; index++)
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
        listItems = new Item[newListItems.Length];

        for (int index = 0; index < newListItems.Length; index++)
        {
            if (newListItems[index] != null)
            {
                Item newItem = (Item)Resources.Load("Prefabs/Items/" + StringUtil.RemoveWhitespace(newListItems[index].name), typeof(Item));
                listItems[index] = newItem;
            }
        }
    }
    #endregion

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && InputUtil.GetAction())
            MerchantController.Instance.Open(this);
    }
}
