using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public bool[] isFull { get; set; } = null;
    public GameObject[] slots { get; set; } = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public void PickUpItem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                isFull[i] = true;
                break;
            }
        }
    }
}
