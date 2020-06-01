﻿using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
public class PlayerManager : CreatureManager
{
    [Header("Components")]
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
            playerAnimator.DistanceAttack();

            GameObject arrow = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector2 shootingDirection;
            if (aim == Vector3.zero)
                shootingDirection = new Vector2(0, -1);
            else
                shootingDirection = new Vector2(InputManager.Instance.GetHorizontal(), InputManager.Instance.GetVertical());

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