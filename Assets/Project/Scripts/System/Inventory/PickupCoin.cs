using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickupCoin : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (InputManager.Instance.GetAction() && collision.CompareTag("Player"))
        {
            PlayerManager.Instance.GetCoin(value);
            Destroy(gameObject);
        }
    }
}
