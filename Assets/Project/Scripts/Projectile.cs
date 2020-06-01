using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb = null;
    [SerializeField]
    private EnumTag targetTag = EnumTag.Enemy;
    private GameObject shooter = null;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float lifetime = 1f;
    private float currentTime = 0;

    public void Init(Vector2 shootingDirection, GameObject _shooter)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = shootingDirection * speed;

        shooter = _shooter;
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag.ToString()))
            collision.GetComponent<CreatureManager>().TakeDamage(damage);

        if (shooter != collision.gameObject && !collision.CompareTag("Projectile"))
            Destroy(gameObject);
    }
}
