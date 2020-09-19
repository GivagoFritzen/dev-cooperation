using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(StaminaManager))]
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class PlayerManager : CreatureManager
{
    public static PlayerManager Instance;

    [Header("Parameters")]
    private Vector2 movement = Vector2.zero;
    [SerializeField]
    public int gold = 0;
    public string transitionPoint { private get; set; } = null;

    [Header("Components")]
    private StaminaManager staminaManager = null;
    [SerializeField]
    private Rigidbody2D rb = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private PlayerAnimator playerAnimator = null;
    [SerializeField]
    private Camera miniMapCamera = null;
    public InventoryObject inventory = null;

    private TextMeshProUGUI lifeText = null;
    [SerializeField]
    private TextMeshProUGUI moneyText = null;

    [Header("References")]
    [SerializeField]
    private GameObject projectile = null;

    #region Default 
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }
    }

    public void Start()
    {
        SetMaxLife();
        SetComponents();

        UpdateUI();
        UpdateMoneyUI();
    }

    private void SetComponents()
    {
        FindComponents();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        staminaManager = gameObject.GetComponent<StaminaManager>();
        staminaManager.Init(moveSpeed);

        if (gameObject.GetComponent<PlayerAnimator>() == null)
            gameObject.AddComponent<PlayerAnimator>();
        else
            playerAnimator = gameObject.GetComponent<PlayerAnimator>();

        playerAnimator.Init(animator);
    }

    private void FindComponents()
    {
        lifeText = GameObject.Find("Life_Text").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("Money_Text").GetComponent<TextMeshProUGUI>();
    }
    #endregion

    #region Load
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu")
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
            if (this != null)
                Destroy(gameObject);
        }
        else
        {
            FindTransitionPoint();

            MiniMapManager.Instance.CheckAndGetCamera(miniMapCamera);
        }
    }

    private void FindTransitionPoint()
    {
        if (!string.IsNullOrEmpty(transitionPoint))
            transform.position = GameObject.Find(transitionPoint).transform.position;

        transitionPoint = null;
    }

    public void Load(PlayerData data)
    {
        gold = data.gold;
        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        InventoryController.Instance.PopulateSlots();
        inventory.Load(data.inventory);
    }
    #endregion

    private void Update()
    {
        Actions();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * staminaManager.GetSpeed() * Time.fixedDeltaTime);
    }

    #region Get/Set
    public void RestoreLife(int restore)
    {
        if (life >= maxLife)
            return;

        life += restore;
        if (life > maxLife)
            life = maxLife;
    }
    #endregion

    #region Actions
    private void Actions()
    {
        if (!playerAnimator.CantAction())
            return;

        movement.x = InputManager.Instance.GetHorizontal();
        movement.y = InputManager.Instance.GetVertical();
        playerAnimator.Movement(movement);

        DistanceAttack();
    }

    private void DistanceAttack()
    {
        Vector3 aim = new Vector3(InputManager.Instance.GetHorizontal(), InputManager.Instance.GetVertical(), 0f);

        if (aim.magnitude > 0f && InputManager.Instance.GetDistanceAttack() ||
            aim.magnitude == 0 && InputManager.Instance.GetDistanceAttack())
        {
            Stop();

            GameObject arrow = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector2 shootingDirection = GetShootingDirection(aim);
            shootingDirection.Normalize();

            arrow.GetComponent<Projectile>().Init(shootingDirection, gameObject, inventory.GetSwordBonus());
            arrow.transform.Rotate(0f, 0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + 90f);
        }
    }

    private Vector2 GetShootingDirection(Vector3 aim)
    {
        if (aim == Vector3.zero)
            return new Vector2(0, -1);
        else
            return new Vector2(InputManager.Instance.GetHorizontal(), InputManager.Instance.GetVertical());
    }

    private void Stop()
    {
        movement = Vector2.zero;
        playerAnimator.DistanceAttack();
    }
    #endregion

    #region UI
    private void UpdateUI()
    {
        lifeText.text = life.ToString();
    }

    public void UpdateMoneyUI()
    {
        moneyText.text = gold.ToString();
    }
    #endregion

    public override void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            GameOverManager.Instance.Actived();
            Destroy(gameObject);
        }
        else
        {
            UpdateUI();
        }
    }
}
