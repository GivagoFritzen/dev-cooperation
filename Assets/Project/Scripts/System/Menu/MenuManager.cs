using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MenuController
{
    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private GameObject credits = null;

    private bool canMove = false;

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

        menu.SetActive(true);
        credits.SetActive(false);

        creditsCoroutine = CreditsController();
    }

    private void Update()
    {
        if (!canMove)
            SelectControllerVertical();
    }

    #region Actions
    public void StartGame(string sceneName)
    {
        canMove = true;
        InitTransition(sceneName);
    }

    public void OpenCredits()
    {
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

            if (InputManager.Instance.GetAction() && creditsDelay > creditsDelayTimer)
            {
                creditsDelay = 0;
                StopCoroutine(creditsCoroutine);

                menu.SetActive(true);
                credits.SetActive(false);

                canMove = false;
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
