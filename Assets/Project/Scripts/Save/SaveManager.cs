using UnityEngine;

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

        Instance = this;
    }

    public void SaveGame()
    {
        SaveSystem.Save();
    }

    public void LoadGame()
    {
        if (PlayerManager.Instance == null)
            Instantiate(playerPrefab);

        if (MenuManagerInGame.Instance != null && MenuManagerInGame.Instance.menu.activeSelf)
            MenuManagerInGame.Instance.ClosePauseMenu();

        ScreenAspectRadio screenAspectRadio = Instantiate(transition).GetComponent<ScreenAspectRadio>();
        screenAspectRadio.Init("", true);
    }
}
