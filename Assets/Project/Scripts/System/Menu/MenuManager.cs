using UnityEngine.SceneManagement;

public class MenuManager : MenuController
{
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        SelectControllerVertical();
    }

    #region Actions
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Load()
    {
        SaveSystem.Load();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    #endregion
}
