using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleEnemy : CreatureManager
{
    [Header("Components")]
    protected NavMeshAgent navMeshAgent = null;

    [Header("Parameters")]
    [SerializeField]
    private float viewDistance = 1;
    [SerializeField]
    private LayerMask layerMask = 0;
    [SerializeField]
    private Vector3 fixedRotation = Vector3.zero;

    [Header("Attack Controller")]
    private bool canAttack = false;
    [SerializeField]
    private float distanceToAttack = 1;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float delayToAttack = 1;
    [SerializeField]
    private float timerAttackController = 0;

    public override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        Stop();
    }

    private void Update()
    {
        FollowPlayer();
        Attack();
        transform.eulerAngles = fixedRotation;
    }

    private void FollowPlayer()
    {
        if (GetPlayerDistance() < viewDistance && CanSeePlayer())
        {
            canAttack = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = PlayerManager.Instance.gameObject.transform.position;
        }
        else
        {
            Stop();
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (PlayerManager.Instance.transform.position - transform.position).normalized;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance, layerMask);
        if (raycastHit2D.collider != null && raycastHit2D.collider.tag == "Player")
            return true;

        return false;
    }

    private void Attack()
    {
        if (!canAttack || GetPlayerDistance() < distanceToAttack && !CanSeePlayer())
            return;

        if (timerAttackController >= delayToAttack)
        {
            timerAttackController = 0;
            PlayerManager.Instance.TakeDamage(damage);
        }
        else
        {
            timerAttackController += Time.deltaTime;
        }
    }

    private float GetPlayerDistance()
    {
        return Vector3.Distance(PlayerManager.Instance.transform.position, gameObject.transform.position);
    }

    private void Stop()
    {
        canAttack = false;
        navMeshAgent.isStopped = true;
        timerAttackController = 1;
    }
}
