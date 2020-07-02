using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    [SerializeField]
    private GameObject playerPrefab = null;

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

        SaveSystem.Load();
    }
}
