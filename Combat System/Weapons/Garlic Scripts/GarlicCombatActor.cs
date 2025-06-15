using UnityEngine;

public class GarlicCombatActor : WeaponActor
{
    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.GarlicActor;


    private float pulseDuration;

    void Start()
    {
        AudioManager.instance.PlayCastGarlicSFX();
        // Get duration from weapon stats
        pulseDuration = GetProjectileDuration();
        Destroy(gameObject, pulseDuration);
    }

    public override void Initialize(WeaponStatModifiers modifiers, float baseDamage)
    {
        base.Initialize(modifiers, baseDamage);
    }


    //void Start()
    //{
    //    AudioManager.instance.PlayWandHitSFX();
    //    Destroy(gameObject, 1f);
    //}
}
