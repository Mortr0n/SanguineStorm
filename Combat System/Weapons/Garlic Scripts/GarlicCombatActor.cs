using UnityEngine;

public class GarlicCombatActor : WeaponActor
{
    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.GarlicActor;

    void Start()
    {
        AudioManager.instance.PlayWandHitSFX();
        Destroy(gameObject, 1f);
    }
}
