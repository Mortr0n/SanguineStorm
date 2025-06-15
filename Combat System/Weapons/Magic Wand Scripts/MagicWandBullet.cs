using UnityEngine;

public class MagicWandBullet : WeaponActor
{
    
    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.MagicWandBulletActor;



    private void Start()
    {
        AudioManager.instance.PlayCastWandSFX();
        damage = damage * projectileMight;
        Destroy(gameObject, 5);
    }
    private void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, projectileSpeed * VS_PlayerCharacterSheet.instance.Stats().projectileSpeed * Time.fixedDeltaTime);
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
