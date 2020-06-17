using UnityEditor;
using UnityEngine;

public class MenuManager : MenuController
{
    public static MenuManager Instance;

    [Header("Menu Manager")]
    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private InventoryManager inventory = null;
    [SerializeField]
    private MenuTag menuTag = MenuTag.Disabled;
    public bool isPaused { get; set; } = false;

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
        pressedButtonMenu = InputManager.Instance.GetMenu();
        pressedButtonInventory = InputManager.Instance.GetInventory();

        if (menu.activeSelf)
            base.SelectControllerVertical();

        MenuController();
        MenuTagController();
    }

    private void MenuTagController()
    {
        if (menuTag == MenuTag.Merchant && (pressedButtonMenu || pressedButtonInventory))
            menuTag = MenuTag.Disabled;
        else if (menuTag != MenuTag.Disabled && (pressedButtonMenu || pressedButtonInventory))
            menuTag = MenuTag.Disabled;
        else if (pressedButtonMenu)
            menuTag = MenuTag.InGame;
        else if (pressedButtonInventory)
            menuTag = MenuTag.Inventory;
    }

    private void MenuController()
    {
        if (MerchantManager.Instance.isOpened && (pressedButtonMenu || pressedButtonInventory))
        {
            MerchantManager.Instance.Close();
            menuTag = MenuTag.Merchant;
            Pause();
        }
        else if (menuTag != MenuTag.Disabled && (pressedButtonMenu || pressedButtonInventory))
        {
            ClosePauseMenu();
        }
        else if (pressedButtonMenu)
        {
            Pause();
            menu.SetActive(isPaused);
        }
        else if (pressedButtonInventory)
        {
            Pause();
            inventory.ActiveMenu(isPaused);
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
        Pause();
        CloseAllMenus();
    }

    private void CloseAllMenus()
    {
        menu.SetActive(false);
        inventory.ActiveMenu(false);
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    #endregion
}
