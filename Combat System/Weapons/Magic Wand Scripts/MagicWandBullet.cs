using UnityEngine;

public class MagicWandBullet : WeaponActor
{
    
    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.MagicWandBulletActor;

    //public override void Initialize(WeaponStatModifiers weaponStatModifiers)
    //{
    //    _weaponStatModifiers = weaponStatModifiers;
    //    //TODO: Test after I've got everything working
    //    if (_weaponStatModifiers == null)
    //    {
    //        Debug.LogWarning("MagicElectricBallActor: weaponStatModifiers is null. Please initialize it before calling Initialize.");
    //        return;
    //    }
    //    if (_weaponStatModifiers != null)
    //    {
    //        SetAttackArea();
    //    }
    //}

    private void Start()
    {
        AudioManager.instance.PlayCastWandSFX();
        damage = damage * GetProjectileMight();
        Destroy(gameObject, 5);
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, GetProjectileSpeed() * VS_PlayerCharacterSheet.instance.Stats().projectileSpeed * Time.fixedDeltaTime);
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

    public override void SetAttackArea()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        var area = GetAttackArea();
        transform.localScale *= area;
        collider.radius *= area * magicColliderMultiple;
    }
}
