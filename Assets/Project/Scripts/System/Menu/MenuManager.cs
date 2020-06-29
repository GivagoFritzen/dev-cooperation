using UnityEngine;

public class MenuManager : MenuController
{
    public static MenuManager Instance;

    [Header("Menu Manager")]
    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private MenuTag menuTag = MenuTag.Disabled;

    private bool pressedButtonMap = false;
    private bool pressedButtonMenu = false;
    private bool pressedButtonInventory = false;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void Start()
    {
        Init();
        CloseAllMenus();
    }

    private void Update()
    {
        ButtonController();

        if (menu.activeSelf)
            SelectControllerVertical();

        MenuController();
        MenuTagController();
    }

    private void ButtonController()
    {
        pressedButtonMap = InputManager.Instance.GetMap();
        pressedButtonMenu = InputManager.Instance.GetMenu();
        pressedButtonInventory = InputManager.Instance.GetInventory();
    }

    private bool PressedAnyButton()
    {
        return pressedButtonMenu || pressedButtonInventory || pressedButtonMap;
    }

    private void MenuTagController()
    {
        if (menuTag == MenuTag.Merchant && PressedAnyButton() ||
            menuTag != MenuTag.Disabled && PressedAnyButton())
            menuTag = MenuTag.Disabled;
        else if (pressedButtonMap)
            menuTag = MenuTag.Map;
        else if (pressedButtonMenu)
            menuTag = MenuTag.InGame;
        else if (pressedButtonInventory)
            menuTag = MenuTag.Inventory;
    }

    private void MenuController()
    {
        if (MerchantController.Instance.isOpened && PressedAnyButton())
        {
            MerchantController.Instance.Close();
            menuTag = MenuTag.Merchant;
            GameManager.Instance.Pause();
        }
        else if (menuTag != MenuTag.Disabled && PressedAnyButton())
        {
            ClosePauseMenu();
        }
        else if (pressedButtonMap)
        {
            MiniMapManager.Instance.Controller();
        }
        else if (pressedButtonMenu)
        {
            GameManager.Instance.Pause();
            menu.SetActive(GameManager.Instance.isPaused);
        }
        else if (pressedButtonInventory)
        {
            GameManager.Instance.Pause();
            InventoryController.Instance.ActiveMenu(GameManager.Instance.isPaused);
        }
    }

    #region Actions
    public void ClosePauseMenuButton()
    {
        ClosePauseMenu();
        menuTag = MenuTag.Disabled;
    }

    private void ClosePauseMenu()
    {
        GameManager.Instance.Pause();
        CloseAllMenus();
    }

    private void CloseAllMenus()
    {
        menu.SetActive(false);
        MiniMapManager.Instance.CloseMap();
        InventoryController.Instance.ActiveMenu(false);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    #endregion
}
