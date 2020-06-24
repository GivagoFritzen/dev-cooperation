using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public bool GetMenu()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public bool GetInventory()
    {
        return Input.GetKeyDown(KeyCode.I);
    }

    public float GetHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool GetSecondUp()
    {
        return Input.GetKey(KeyCode.O);
    }

    public bool GetSecondDown()
    {
        return Input.GetKey(KeyCode.L);
    }

    public bool GetDistanceAttack()
    {
        return Input.GetKeyDown(KeyCode.Alpha1);
    }

    public bool GetMap()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public bool GetAction()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
