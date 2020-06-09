using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(BoxCollider2D))]
public class CreatureManager : MonoBehaviour
{
    [Header("Components")]
    protected Rigidbody2D rb = null;
    protected Animator animator = null;

    [Header("Parameters")]
    [SerializeField]
    protected int maxLife = 1;
    protected int life = 1;
    [SerializeField]
    protected float moveSpeed = 5f;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        life = maxLife;
    }

    public virtual void TakeDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
            Destroy(gameObject);
    }
}
