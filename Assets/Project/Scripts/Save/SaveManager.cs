using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField]
    private GameObject playerPrefab = null;
    [SerializeField]
    private GameObject transition = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void SaveGame()
    {
        if (!SceneManager.GetActiveScene().name.Contains("Dungeon"))
            SaveSystem.Save();
    }

    public void LoadGame()
    {
        if (PlayerManager.Instance == null)
            Instantiate(playerPrefab);

        if (MenuManagerInGame.Instance != null && MenuManagerInGame.Instance.menu.activeSelf)
            MenuManagerInGame.Instance.ClosePauseMenuButton();

        ScreenAspectRadio screenAspectRadio = Instantiate(transition).GetComponent<ScreenAspectRadio>();
        screenAspectRadio.Init("", true);
    }
}
