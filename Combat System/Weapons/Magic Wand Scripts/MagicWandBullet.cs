using UnityEngine;

public class MagicWandBullet : WeaponActor
{
    [SerializeField] float bulletSpeed = 2.5f;
    Vector3 moveDirection = Vector3.zero;
    private void Start()
    {
        AudioManager.instance.PlayCastWandSFX();
        damage = damage * VS_PlayerCharacterSheet.instance.Stats().might;
        Destroy(gameObject, 5);
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, bulletSpeed * VS_PlayerCharacterSheet.instance.Stats().projectileSpeed * Time.fixedDeltaTime);
    }
    public override void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        moveDirection = target.transform.position - transform.position;
    }

    protected override void HitReceiver(CombatReceiver2D target)
    {
        AudioManager.instance.PlayWandHitSFX();
        base.HitReceiver(target);
        Destroy(gameObject);
    }
}
