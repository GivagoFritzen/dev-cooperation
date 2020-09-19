using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public static MiniMapManager Instance;

    [SerializeField]
    private Vector2 limitMap = Vector2.zero;
    private Camera mapCamera = null;
    [SerializeField]
    private float zDistance = 10;
    [SerializeField]
    private float speed = 1;
    private float horizontal = 0;
    private float vertical = 0;
    private Vector3 target = Vector3.zero;
    private GameObject cameraRepresentation = null;
    [SerializeField]
    private float interactionRadius = 1;

    [SerializeField]
    private float maxZoom = 0;
    [SerializeField]
    private float minZoom = 0;
    private float up = 0;
    private float down = 0;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void CheckAndGetCamera(Camera camera = null)
    {
        if (camera != null)
        {
            mapCamera = camera;
            cameraRepresentation = mapCamera.transform.GetChild(0).gameObject;
            mapCamera.gameObject.SetActive(false);
        }
        else if (mapCamera == null)
        {
            mapCamera = GameObject.Find("Map Camera").GetComponent<Camera>();
            cameraRepresentation = mapCamera.transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.PlayerIsAlive() || !mapCamera.gameObject.activeSelf)
            return;

        Movement();
        Zoom();

        if (InputManager.Instance.GetAction())
            Travel();
    }

    #region Movement
    private void Movement()
    {
        horizontal = InputManager.Instance.GetHorizontal();
        vertical = InputManager.Instance.GetVertical();

        if (horizontal != 0 || vertical != 0)
        {
            target = mapCamera.transform.localPosition + new Vector3(horizontal, vertical, 0) * speed * Time.unscaledDeltaTime;

            target.x = GetLimitX();
            target.y = GetLimitY();
            target.z = -zDistance;

            mapCamera.transform.localPosition = target;
        }
    }

    private float GetLimitX()
    {
        if (target.x > limitMap.x)
            return limitMap.x;
        else if (target.x < -limitMap.x)
            return -limitMap.x;
        else
            return target.x;
    }

    private float GetLimitY()
    {
        if (target.y > limitMap.y)
            return limitMap.y;
        else if (target.y < -limitMap.y)
            return -limitMap.y;
        else
            return target.y;
    }
    #endregion

    private void Zoom()
    {
        if (InputManager.Instance.GetSecondUp())
            up = speed;
        else
            up = 0;

        if (InputManager.Instance.GetSecondDown())
            down = speed;
        else
            down = 0;

        if (up != 0)
        {
            mapCamera.orthographicSize += up * Time.unscaledDeltaTime;
            if (mapCamera.orthographicSize > maxZoom)
                mapCamera.orthographicSize = maxZoom;
        }
        else if (down != 0)
        {
            mapCamera.orthographicSize -= down * Time.unscaledDeltaTime;
            if (mapCamera.orthographicSize < minZoom)
                mapCamera.orthographicSize = minZoom;
        }
    }

    private void Travel()
    {
        Collider2D[] objectsAroundMe = Physics2D.OverlapCircleAll(cameraRepresentation.transform.position, interactionRadius);

        foreach (var obj in objectsAroundMe)
        {
            if (obj.CompareTag("Map-FastTravel"))
            {
                PlayerManager.Instance.transform.position = obj.transform.position;
                MenuManagerInGame.Instance.ClosePauseMenuButton();
            }
        }
    }

    #region Controller Menu
    public void Controller()
    {
        if (GameManager.Instance.isPaused)
            CloseMap();
        else
            OpenMap();

        GameManager.Instance.Pause();
    }

    private void OpenMap()
    {
        mapCamera.gameObject.SetActive(true);
        mapCamera.gameObject.transform.localPosition = new Vector3(0, 0, -zDistance);
    }

    public void CloseMap()
    {
        mapCamera.gameObject.SetActive(false);
        mapCamera.transform.position = new Vector3(PlayerManager.Instance.transform.position.x, PlayerManager.Instance.transform.position.y, -zDistance);
    }
    #endregion
}
