using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D), typeof(Animator))]
public class WalkerManager : CreatureManager
{
    [Header("Components")]
    [SerializeField]
    private Rigidbody2D rb = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private TextMeshProUGUI lifeText = null;
    [SerializeField]
    private PlayerAnimator playerAnimator = null;

    [Header("Parameters")]
    private Vector2 movement = Vector2.zero;

    [Header("References")]
    [SerializeField]
    private GameObject projectile = null;

    public override void Start()
    {
        base.Start();
        UpdateUI();
        SetComponents();
    }

    private void SetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (gameObject.GetComponent<PlayerAnimator>() == null)
            gameObject.AddComponent<PlayerAnimator>();
        else
            playerAnimator = gameObject.GetComponent<PlayerAnimator>();

        playerAnimator.Init(animator);
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
        base.TakeDamage(damage);
        UpdateUI();
    }
}
