using UnityEngine;

public class CreatureManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    protected int maxLife = 1;
    public int life { get; set; } = 1;
    [SerializeField]
    protected float moveSpeed = 5f;

    public void SetMaxLife()
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
