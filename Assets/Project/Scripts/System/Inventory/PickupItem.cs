using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private Item item = null;

    private void Start()
    {
        if (item == null)
            Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputManager.Instance.GetAction() && collision.CompareTag("Player"))
        {
            bool taked = PlayerManager.Instance.inventory.PickUpItem(item);

            if (taked)
                Destroy(gameObject);
        }
    }
}
