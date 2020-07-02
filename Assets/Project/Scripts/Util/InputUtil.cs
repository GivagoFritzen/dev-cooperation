using UnityEngine;

public static class InputUtil
{
    public static bool GetMenu()
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }

    public static bool GetInventory()
    {
        return Input.GetKeyDown(KeyCode.I);
    }

    public static float GetHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public static float GetVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public static bool GetSecondUp()
    {
        return Input.GetKey(KeyCode.O);
    }

    public static bool GetSecondDown()
    {
        return Input.GetKey(KeyCode.L);
    }

    public static bool GetDistanceAttack()
    {
        return Input.GetKeyDown(KeyCode.Alpha1);
    }

    public static bool GetMap()
    {
        return Input.GetKeyDown(KeyCode.M);
    }

    public static bool GetAction()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
