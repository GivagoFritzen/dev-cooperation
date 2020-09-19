using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float distanceExplosion = 1;
    [SerializeField]
    private int damage = 1;

    public void Init()
    {
        if (GetPlayerDistance() < distanceExplosion)
            PlayerManager.Instance.TakeDamage(damage);

        Destroy(gameObject);
    }

    private float GetPlayerDistance()
    {
        return Vector3.Distance(PlayerManager.Instance.transform.position, gameObject.transform.position);
    }
}
