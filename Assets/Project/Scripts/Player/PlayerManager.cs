using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class PlayerManager : CreatureManager
{
    public static PlayerManager Instance;

    [Header("Parameters")]
    private Vector2 movement = Vector2.zero;
    [SerializeField]
    public int gold = 0;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rb = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private TextMeshProUGUI lifeText = null;
    [SerializeField]
    private PlayerAnimator playerAnimator = null;
    public InventoryObject inventory = null;

    [Header("References")]
    [SerializeField]
    private GameObject projectile = null;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    public override void Start()
    {
        base.Start();
        SetComponents();
        UpdateUI();
    }

    private void SetComponents()
    {
        FindComponents();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (gameObject.GetComponent<PlayerAnimator>() == null)
            gameObject.AddComponent<PlayerAnimator>();
        else
            playerAnimator = gameObject.GetComponent<PlayerAnimator>();

        playerAnimator.Init(animator);
    }

    private void FindComponents()
    {
        lifeText = GameObject.Find("Life_Text").GetComponent<TextMeshProUGUI>();
    }

    public void Load(PlayerData data)
    {
        gold = data.gold;
        transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);

        InventoryController.Instance.PopulateInventorySlots();
        inventory.Load(data.inventory);
    }

    private void Update()
    {
        Actions();
    }

    private void UpdateUI()
    {
        lifeText.text = life.ToString();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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

        movement.x = InputUtil.GetHorizontal();
        movement.y = InputUtil.GetVertical();
        playerAnimator.Movement(movement);

        DistanceAttack();
    }

    private void DistanceAttack()
    {
        Vector3 aim = new Vector3(InputUtil.GetHorizontal(), InputUtil.GetVertical(), 0f);

        if (aim.magnitude > 0f && InputUtil.GetDistanceAttack() ||
            aim.magnitude == 0 && InputUtil.GetDistanceAttack())
        {
            Stop();
            playerAnimator.DistanceAttack();

            GameObject arrow = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector2 shootingDirection;
            if (aim == Vector3.zero)
                shootingDirection = new Vector2(0, -1);
            else
                shootingDirection = new Vector2(InputUtil.GetHorizontal(), InputUtil.GetVertical());

            shootingDirection.Normalize();
            arrow.GetComponent<Projectile>().Init(shootingDirection, gameObject);
            arrow.transform.Rotate(0f, 0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg + 90f);
        }
    }

    private void Stop()
    {
        movement = Vector2.zero;
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
