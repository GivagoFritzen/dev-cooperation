using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenAspectRadio : TransitionDefault
{
    public static ScreenAspectRadio Instance;

    public bool isOpened { private get; set; } = false;
    private bool isLoaded = false;
    private string sceneName = "";
    private float radius = 0;
    private float counter = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private RectTransform canvas = null;
    [SerializeField]
    private Image maskTransition = null;

    private float screen_h = 0;
    private float screen_w = 0;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        isOpened = true;
        SetInitRadius();
    }

    public void Init(string sceneName = "", bool isLoaded = false)
    {
        Time.timeScale = 0f;
        isOpened = false;

        this.sceneName = sceneName;
        this.isLoaded = isLoaded;

        SetInitRadius();

        canvas = GetComponent<RectTransform>();
        screen_h = Screen.height;
        screen_w = Screen.width;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(GetTarget());
        float characterScreen_h;
        float characterScreen_w;

        if (IsPortrait())
        {
            maskTransition.rectTransform.sizeDelta = new Vector2(canvas.rect.height, canvas.rect.height);

            float newScreenPos_x = screenPos.x + (screen_h - screen_w) / 2;
            characterScreen_w = newScreenPos_x * 100 / screen_h;
            characterScreen_w /= 100;

            characterScreen_h = screenPos.y * 100 / screen_h;
            characterScreen_h /= 100;
        }
        else
        {
            maskTransition.rectTransform.sizeDelta = new Vector2(canvas.rect.width, canvas.rect.width);

            characterScreen_w = screenPos.x * 100 / screen_w;
            characterScreen_w /= 100;

            float newScreenPos_y = screenPos.y + (screen_w - screen_h) / 2;
            characterScreen_h = newScreenPos_y * 100 / screen_w;
            characterScreen_h /= 100;
        }

        maskTransition.material.SetFloat("Center_X", characterScreen_w);
        maskTransition.material.SetFloat("Center_Y", characterScreen_h);
    }

    private void Update()
    {
        counter += Time.unscaledDeltaTime * speed;

        if (counter <= 0.5)
            return;
        else if (isOpened)
            IncreaseRadius();
        else
            DecreaseRadius();
    }

    private void IncreaseRadius()
    {
        if (radius < 1)
        {
            radius += Time.unscaledDeltaTime * speed;
            maskTransition.material.SetFloat("Radius", radius);

            if (radius >= 1)
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }
    }

    private void DecreaseRadius()
    {
        if (radius > 0)
        {
            radius -= Time.unscaledDeltaTime * speed;
            maskTransition.material.SetFloat("Radius", radius);

            if (radius <= 0)
            {
                if (isLoaded)
                    SaveSystem.Load();
                else
                    StartGame(sceneName);
            }
        }
    }

    private Vector3 GetTarget()
    {
        if (PlayerManager.Instance != null)
            return PlayerManager.Instance.transform.position;

        return Camera.main.transform.position;
    }

    private void SetInitRadius()
    {
        if (isOpened)
            radius = 0;
        else
            radius = 1;

        maskTransition.material.SetFloat("Radius", radius);
    }

    private bool IsPortrait()
    {
        return screen_w < screen_h;
    }
}
