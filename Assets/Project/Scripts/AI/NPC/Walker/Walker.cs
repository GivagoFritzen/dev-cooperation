using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Walker : MonoBehaviour
{
    [Header("Components")]
    protected NavMeshAgent navMeshAgent = null;

    [Header("Parameters")]
    [SerializeField]
    private float moveSpeed = 1;
    [SerializeField]
    private Vector3 fixedRotation = Vector3.zero;

    [Header("Ways Controller")]
    [SerializeField]
    private float distanceToFindAnotherPoint = 1;
    [SerializeField]
    private GameObject[] ways = null;

    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private GameObject currentWay = null;
    [SerializeField]
    private int currentPoint = -1;
    [SerializeField]
    private GameObject target = null;

    public void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        FindNewWay();
        FindNextPoint();
    }

    private void Update()
    {
        transform.eulerAngles = fixedRotation;

        if (!canMove || ways == null || currentWay == null)
            return;

        if (GetPointDistance() < distanceToFindAnotherPoint)
            FindNextPoint();

        navMeshAgent.destination = target.transform.position;
    }

    private float GetPointDistance()
    {
        return Vector3.Distance(target.transform.position, gameObject.transform.position);
    }

    private void FindNextPoint()
    {
        currentPoint += 1;

        if (currentPoint >= currentWay.transform.childCount)
            FindNewWay();

        target = currentWay.transform.GetChild(currentPoint).gameObject;
    }

    private void FindNewWay()
    {
        currentPoint = 0;
        int indexNewWay = Random.Range(0, ways.Length);
        currentWay = ways[indexNewWay];
    }

    public void Walk()
    {
        canMove = true;
        navMeshAgent.isStopped = false;
    }

    public void Stop()
    {
        canMove = false;
        navMeshAgent.isStopped = true;
    }
}
