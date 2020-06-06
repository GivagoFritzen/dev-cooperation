using UnityEditor;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public bool isPaused { get; set; } = false;
    private MenuTag menuTag = MenuTag.Disabled;
    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private InventoryManager inventory = null;

    [Header("Inputs Controller")]
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
        CloseAllMenus();
    }

    private void Update()
    {
        pressedButtonMenu = InputManager.Instance.GetMenu();
        pressedButtonInventory = InputManager.Instance.GetInventory();

        MenuController();
        MenuTagController();
    }

    private void MenuTagController()
    {
        if (menuTag != MenuTag.Disabled && (pressedButtonMenu || pressedButtonInventory))
            menuTag = MenuTag.Disabled;
        else if (pressedButtonMenu)
            menuTag = MenuTag.InGame;
        else if (pressedButtonInventory)
            menuTag = MenuTag.Inventory;
    }

    private void MenuController()
    {
        if (menuTag != MenuTag.Disabled && (pressedButtonMenu || pressedButtonInventory))
        {
            Pause();
            CloseAllMenus();
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
}
