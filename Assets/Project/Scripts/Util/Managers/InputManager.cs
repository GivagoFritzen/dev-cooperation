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

    public float GetHorizontal()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVertical()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool GetDistanceAttack()
    {
        return Input.GetKeyDown(KeyCode.Alpha1);
    }

    public bool GetStartDialogue()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool GetNextDialogue()
    {
        return Input.GetKeyDown(KeyCode.F);
    }
}
