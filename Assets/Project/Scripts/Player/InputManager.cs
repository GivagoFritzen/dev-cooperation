using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerControls controls = null;

    private Vector2 movement = Vector2.zero;

    private bool secondUp = false;
    private bool secondDown = false;
    private bool menu = false;
    private bool inventory = false;
    private bool distanceAttack = false;
    private bool map = false;
    private bool action = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            ConfigurationControls();
        }
    }

    #region InputAction Config
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void ConfigurationControls()
    {
        controls = new PlayerControls();

        MovementControl();
        SecondAxisControl();

        MenuControl();
        InventoryControl();
        DistanceAttackControl();
        MapControl();
        ActionControl();
    }

    private void MovementControl()
    {
        controls.Instance.Movement.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Instance.Movement.canceled += ctx => movement = Vector2.zero;
    }

    private void SecondAxisControl()
    {
        controls.Instance.SecondUp.performed += ctx => secondUp = true;
        controls.Instance.SecondUp.canceled += ctx => secondUp = false;

        controls.Instance.SecondDown.performed += ctx => secondDown = true;
        controls.Instance.SecondDown.canceled += ctx => secondDown = false;
    }

    private void MenuControl()
    {
        controls.Instance.Menu.started += ctx => menu = !menu;
    }

    private void InventoryControl()
    {
        controls.Instance.Inventory.started += ctx => inventory = !inventory;
    }

    private void DistanceAttackControl()
    {
        controls.Instance.DistanceAttack.performed += ctx => distanceAttack = !distanceAttack;
    }

    private void MapControl()
    {
        controls.Instance.Map.started += ctx => map = !map;
    }

    private void ActionControl()
    {
        controls.Instance.Action.started += ctx => action = !action;
    }
    #endregion

    public float GetHorizontal()
    {
        return movement.x;
    }

    public float GetVertical()
    {
        return movement.y;
    }

    public bool GetSecondUp()
    {
        return secondUp;
    }

    public bool GetSecondDown()
    {
        return secondDown;
    }

    public bool GetMenu()
    {
        bool currentMenu = menu;
        menu = false;
        return currentMenu;
    }

    public bool GetInventory()
    {
        bool currentInventory = inventory;
        inventory = false;
        return currentInventory;
    }

    public bool GetDistanceAttack()
    {
        bool currentDistanceAttack = distanceAttack;
        distanceAttack = false;
        return currentDistanceAttack;
    }

    public bool GetMap()
    {
        bool currentMap = map;
        map = false;
        return currentMap;
    }

    public bool GetAction()
    {
        bool currentAction = action;
        action = false;
        return currentAction;
    }
}
