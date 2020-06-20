using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Merchant : MonoBehaviour
{
    [SerializeField]
    private Item[] listItems = null;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && InputManager.Instance.GetAction())
            MerchantController.Instance.Open(listItems);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Item[] newList = MerchantController.Instance.Close();
            if (newList != null)
                listItems = newList;
        }
    }
}
