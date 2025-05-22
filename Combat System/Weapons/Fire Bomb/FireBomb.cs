using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FireBomb : WeaponActor
{
    private bool hasExploded = false;

    private void Start()
    {
        damage = damage * VS_PlayerCharacterSheet.instance.Stats().might;
        Destroy(gameObject, 1f);
    }


    protected override void HitReceiver(CombatReceiver2D target)
    {
        if (hasExploded) return;

        base.HitReceiver(target);
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
