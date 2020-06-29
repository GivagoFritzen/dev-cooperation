using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

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
        Camera.main.gameObject.transform.SetParent(this.gameObject.transform);
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

    private void CloseAllCanvas()
    {
        foreach (var currentCanvas in canvasToDisable)
            currentCanvas.SetActive(false);
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

    #region Buttons
    public void Load()
    {
        Disabled();
        SaveManager.Instance.LoadGame();
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    #endregion
}
