using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookManager : MonoBehaviour
{
    public static BookManager Instance;

    [SerializeField]
    private Image imageAnchor = null;
    private TextUI content = null;
    private GameObject bookObject = null;

    [Header("Sprites")]
    [SerializeField]
    private Sprite bookClosedFront = null;
    [SerializeField]
    private Sprite bookOpened = null;
    [SerializeField]
    private Sprite bookClosedBack = null;

    [Header("Controllers")]
    [SerializeField]
    private TextMeshProUGUI pageLeft = null;
    [SerializeField]
    private TextMeshProUGUI pageRight = null;
    [SerializeField]
    private int currentPage = 0;

    [SerializeField]
    protected float delay = 0.2f;
    protected float inputController = 0;
    protected float horizontal = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        currentPage = 0;
        ChangePage();

        bookObject = gameObject.transform.GetChild(0).gameObject;
        bookObject.SetActive(false);
    }

    public void Active(TextUI content)
    {
        currentPage = 0;
        ChangePage();

        this.content = content;
        bookObject.SetActive(true);

        MenuManagerInGame.Instance.CloseAllMenus();
    }

    public void Close()
    {
        bookObject.SetActive(false);
    }

    private void OnEnable()
    {
        currentPage = 0;
        ChangePage();
    }

    private void Update()
    {
        if (bookObject.activeSelf)
            InputControllerHorizontal();
    }

    private void InputControllerHorizontal()
    {
        if (inputController < delay)
        {
            inputController += Time.unscaledDeltaTime;
            return;
        }

        horizontal = InputManager.Instance.GetHorizontal();

        if (horizontal == 0)
            return;
        else if (horizontal > 0 && currentPage < GetPagesAmount() + 1)
            currentPage += 1;
        else if (horizontal < 0 && currentPage > 0)
            currentPage -= 1;

        ChangePage();
        WritePage();
        inputController = 0;
    }

    #region Pages
    private void ChangePage()
    {
        if (currentPage == 0)
            imageAnchor.sprite = bookClosedFront;
        else if (currentPage <= GetPagesAmount())
            imageAnchor.sprite = bookOpened;
        else
            imageAnchor.sprite = bookClosedBack;
    }

    private int GetPagesAmount()
    {
        return Mathf.CeilToInt((float)content.GetCurrentTextLanguage().sentences.Length / 2);
    }

    private void WritePage()
    {
        pageLeft.text = "";
        pageRight.text = "";

        if (currentPage == 0 || currentPage > GetPagesAmount())
            return;

        bool isOdd = false;
        if (currentPage == GetPagesAmount())
            isOdd = content.GetCurrentTextLanguage().sentences.Length % 2 != 0;

        if (isOdd)
        {
            pageLeft.text = content.GetCurrentTextLanguage().sentences[(currentPage - 1) * 2].ToString();
        }
        else
        {
            pageLeft.text = content.GetCurrentTextLanguage().sentences[currentPage / 2].ToString();
            pageRight.text = content.GetCurrentTextLanguage().sentences[currentPage / 2 + 1].ToString();
        }
    }
    #endregion
}
