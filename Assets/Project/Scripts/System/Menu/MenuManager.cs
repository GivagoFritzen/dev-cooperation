using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MenuController
{
    private float creditsDelay = 0;
    [SerializeField]
    private float creditsDelayTimer = .5f;

    [SerializeField]
    private GameObject menu = null;
    [SerializeField]
    private GameObject credits = null;
    private IEnumerator creditsCoroutine = null;

    private void Start()
    {
        Init();

        menu.SetActive(true);
        credits.SetActive(false);

        creditsCoroutine = CreditsController();
    }

    private void Update()
    {
        if (creditsDelay == 0)
            SelectControllerVertical();
    }

    #region Actions
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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

            if (Input.anyKey && creditsDelay > creditsDelayTimer)
            {
                creditsDelay = 0;
                StopCoroutine(creditsCoroutine);
                menu.SetActive(true);
                credits.SetActive(false);
            }

            yield return null;
        }
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
