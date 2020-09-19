using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MenuController
{
    public static GameOverManager Instance;

    [Header("Game Over Menu")]
    [SerializeField]
    private GameObject background = null;
    private Image backgroundImage = null;
    [SerializeField]
    private float fadeSpeed = 1;
    [SerializeField]
    private GameObject gameOverMenu = null;
    [SerializeField]
    private GameObject[] canvasToDisable = null;
    private new Camera camera = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        backgroundImage = background.GetComponent<Image>();
        Disabled();
    }

    private void Update()
    {
        if (backgroundImage.color.a >= 1)
            SelectControllerVertical();
        else
            FadeOut();
    }

    public void Actived()
    {
        enabled = true;
        camera = Camera.main;
        camera.gameObject.transform.SetParent(this.gameObject.transform);
        CloseAllCanvas();
        background.SetActive(true);
    }

    private void Disabled()
    {
        Color tempColor = backgroundImage.color;
        tempColor.a = 0;
        backgroundImage.color = tempColor;
        background.SetActive(false);

        gameOverMenu.SetActive(false);
        enabled = false;
    }

    private void FadeOut()
    {
        if (backgroundImage.color.a < 1)
        {
            Color tempColor = backgroundImage.color;
            tempColor.a += Time.deltaTime * fadeSpeed;
            backgroundImage.color = tempColor;

            if (backgroundImage.color.a >= 1)
            {
                gameOverMenu.SetActive(true);
                Init();
            }
        }
    }

    #region Canvas Controller
    private void OpenAllCanvas()
    {
        foreach (var currentCanvas in canvasToDisable)
            currentCanvas.SetActive(true);
    }

    private void CloseAllCanvas()
    {
        foreach (var currentCanvas in canvasToDisable)
            currentCanvas.SetActive(false);
    }
    #endregion

    #region Buttons
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Load()
    {
        Disabled();
        Destroy(camera.gameObject);
        OpenAllCanvas();
        SaveManager.Instance.LoadGame();
        MiniMapManager.Instance.CheckAndGetCamera();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    #endregion
}
