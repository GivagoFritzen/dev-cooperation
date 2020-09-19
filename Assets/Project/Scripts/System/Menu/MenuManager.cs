using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MenuController
{
    [Space(10)]
    [Header("Menu Manager")]
    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private GameObject options = null;
    [SerializeField]
    private GameObject credits = null;

    public bool canMove { get; set; } = false;

    private float creditsDelay = 0;
    [SerializeField]
    private float creditsDelayTimer = .5f;
    private IEnumerator creditsCoroutine = null;

    [SerializeField]
    private GameObject transition = null;
    private ScreenAspectRadio screenAspectRadio = null;

    private void Start()
    {
        Init();
        canMove = true;

        menu.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);

        creditsCoroutine = CreditsController();
    }

    protected override void Init()
    {
        base.Init();

        if (ManagersControl.Instance != null)
            ManagersControl.Instance.Destroy();
    }

    private void Update()
    {
        if (canMove)
            SelectControllerVertical();
    }

    #region Actions
    public void StartGame(string sceneName)
    {
        canMove = false;
        InitTransition(sceneName);
    }

    public void OpenOptions()
    {
        canMove = false;
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void OpenCredits()
    {
        canMove = false;
        menu.SetActive(false);
        credits.SetActive(true);

        creditsDelay = 0;
        CreditsManager.Instance.Open();

        StartCoroutine(creditsCoroutine);
    }

    IEnumerator CreditsController()
    {
        while (true)
        {
            creditsDelay += Time.deltaTime;

            if (InputManager.Instance.AnyKey() && creditsDelay > creditsDelayTimer)
            {
                creditsDelay = 0;
                StopCoroutine(creditsCoroutine);

                menu.SetActive(true);
                credits.SetActive(false);

                canMove = true;
            }

            yield return null;
        }
    }

    public void Load()
    {
        InitTransition("", true);
    }

    private void InitTransition(string sceneName = "", bool isLoaded = false)
    {
        screenAspectRadio = Instantiate(transition).GetComponent<ScreenAspectRadio>();
        screenAspectRadio.Init(sceneName, isLoaded);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    #endregion
}
