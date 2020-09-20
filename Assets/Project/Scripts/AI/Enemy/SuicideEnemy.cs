using UnityEngine;

public class SuicideEnemy : SimpleEnemy
{
    [Header("Suicide Enemy")]
    private bool explode = false;
    [SerializeField]
    private float explodeTimerDelay = 0;
    private float explodeTimerDelayController = 0;

    [Header("Explosion")]
    [SerializeField]
    private GameObject explodeParticlePrefab = null;
    [SerializeField]
    private float distanceExplosion = 1;

    [Header("Material Controller")]
    private Material matWhite = null;
    private Material matDefault = null;
    private SpriteRenderer spriteRenderer = null;

    private float delayToChangeMaterialController = 0;
    [SerializeField]
    private float delayToChangeMaterial = 1;

    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        matDefault = spriteRenderer.material;
        matWhite = Resources.Load("Material/WhiteFlash", typeof(Material)) as Material;
    }

    protected override void Update()
    {
        if (explode == false)
            base.Update();
        else
            ChangeMaterial();
    }

    protected override void Attack()
    {
        explode = true;
        Stop();
        FixedRotation();
    }

    private void ChangeMaterial()
    {
        delayToChangeMaterialController += Time.deltaTime;

        if (delayToChangeMaterialController >= delayToChangeMaterial)
        {
            if (spriteRenderer.material == matDefault)
                spriteRenderer.material = matWhite;
            else
                spriteRenderer.material = matDefault;

            explodeTimerDelayController += 1;
            delayToChangeMaterialController = 0;
        }

        CreateParticleExplosion();
    }

    private void CreateParticleExplosion()
    {
        if (explodeTimerDelay >= explodeTimerDelayController)
        {
            GameObject particles = Instantiate(explodeParticlePrefab);
            particles.transform.position = transform.position;

            if (GetPlayerDistance() < distanceExplosion)
                PlayerManager.Instance.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    private float GetPlayerDistance()
    {
        return Vector3.Distance(PlayerManager.Instance.transform.position, gameObject.transform.position);
    }
}
