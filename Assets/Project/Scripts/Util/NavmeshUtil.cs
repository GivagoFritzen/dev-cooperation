using UnityEngine;
using UnityEngine.AI;

public class NavmeshUtil : MonoBehaviour
{
    public Vector3 RandomNavmeshLocationInsideSphere(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;

        NavMeshHit hit;
        Vector3 finalPosition = transform.position;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            finalPosition = hit.position;

        return finalPosition;
    }
}
