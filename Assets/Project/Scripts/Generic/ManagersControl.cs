using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ManagersControl : MonoBehaviour
{
    public static ManagersControl Instance;
    private NavMeshSurface2d navMeshSurface2d = null;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            Instance = this;
        }
    }

    public void Destroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
        Instance = null;
        Destroy(gameObject);
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        bool isDungeonScene = scene.name.Contains("Dungeon");

        GetNavMeshSurface();
        InventoryController.Instance.GetComponents();
        MenuManagerInGame.Instance.ShowSaveButton(isDungeonScene);
        DayNightManager.Instance.Config(isDungeonScene);
        DestroyOthersManagersControl();
    }

    public void GetNavMeshSurface()
    {
        if (navMeshSurface2d != null)
            Destroy(navMeshSurface2d.gameObject);

        navMeshSurface2d = FindObjectOfType<NavMeshSurface2d>();
        navMeshSurface2d.gameObject.transform.parent = transform;
        navMeshSurface2d.enabled = true;
    }

    private void DestroyOthersManagersControl()
    {
        foreach (var managersControl in FindObjectsOfType<ManagersControl>())
            if (this != managersControl)
                Destroy(managersControl.gameObject);
    }
}
