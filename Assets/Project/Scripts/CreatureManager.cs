using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    protected int maxLife = 1;
    [SerializeField]
    protected int life = 1;
    [SerializeField]
    protected float moveSpeed = 5f;

    public virtual void Start()
    {
        life = maxLife;
    }

    public virtual void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
            Destroy(gameObject);
    }
}
