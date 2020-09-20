using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SimpleEnemy : CreatureManager
{
    [Header("Components")]
    protected NavMeshAgent navMeshAgent = null;
    private NavmeshUtil navmeshUtil = null;

    [Header("Parameters")]
    [SerializeField]
    public new string name = "";
    [SerializeField]
    private float viewDistance = 1;
    [SerializeField]
    private LayerMask layerMask = 0;
    [SerializeField]
    private Vector3 fixedRotation = Vector3.zero;

    [Header("Find Random Point in Navmesh")]
    [SerializeField]
    private float radiusToFindAnotherPointInNavmesh = 1;
    [SerializeField]
    private float timeToFindAnotherPoint = 1;
    [SerializeField]
    private float timeToFindAnotherPointController = 0;
    private float distanceToStopPoint = 2;
    [SerializeField]
    private Vector3 nextPoint = Vector3.one;

    [Header("Attack Controller")]
    [SerializeField]
    private float distanceToAttack = 1;
    private bool canAttack = false;
    [SerializeField]
    protected int damage = 1;
    [SerializeField]
    private float delayToAttack = 1;
    [SerializeField]
    private float timerAttackController = 0;

    protected virtual void Start()
    {
        SetMaxLife();

        navmeshUtil = gameObject.AddComponent<NavmeshUtil>();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        FindNewPoint();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.PlayerIsAlive() && GetPlayerDistance() < viewDistance && CanSeePlayer())
        {
            FollowPlayer();
            Attack();
        }
        else
        {
            TimerControllerFindAnotherPointInNavmesh();
        }

        FixedRotation();
    }

    protected void FixedRotation()
    {
        transform.eulerAngles = fixedRotation;
    }

    #region Find Random Point
    private void TimerControllerFindAnotherPointInNavmesh()
    {
        if (Vector3.Distance(nextPoint, gameObject.transform.position) < distanceToStopPoint)
        {
            Stop();
            timeToFindAnotherPointController += Time.deltaTime;

            if (timeToFindAnotherPointController >= timeToFindAnotherPoint)
                FindNewPoint();
        }
    }

    private void FindNewPoint()
    {
        timeToFindAnotherPointController = 0;

        navMeshAgent.isStopped = false;
        nextPoint = navmeshUtil.RandomNavmeshLocationInsideSphere(radiusToFindAnotherPointInNavmesh);
        navMeshAgent.destination = nextPoint;
    }
    #endregion

    #region Player
    private void FollowPlayer()
    {
        canAttack = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.destination = PlayerManager.Instance.gameObject.transform.position;
    }

    private bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (PlayerManager.Instance.transform.position - transform.position).normalized;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance, layerMask);

        if (raycastHit2D.collider != null && raycastHit2D.collider.tag == "Player")
            return true;

        return false;
    }

    protected virtual void Attack()
    {
        if (!canAttack || GetPlayerDistance() > distanceToAttack || !CanSeePlayer())
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
    #endregion

    protected void Stop()
    {
        canAttack = false;
        navMeshAgent.isStopped = true;
        timerAttackController = 1;
    }
}
