using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FireBomb : WeaponActor
{
    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.FireBombActor;
    public WeaponStatModifiers _weaponStatModifiers;
    private bool hasExploded = false;

    private void Start()
    {
        damage = damage * VS_PlayerCharacterSheet.instance.Stats().might;
        Destroy(gameObject, 1f);
    }

    public override void Initialize(WeaponStatModifiers weaponStatModifiers, float baseDamage)
    {
        base.Initialize(weaponStatModifiers, baseDamage);
    }

    


    protected override void HitReceiver(CombatReceiver2D target)
    {
        if (hasExploded) return;

        base.HitReceiver(target);
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(GetProjectileDuration());
        Destroy(gameObject);
    }
}
