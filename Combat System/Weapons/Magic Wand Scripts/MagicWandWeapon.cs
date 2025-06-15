using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(CircleCollider2D))]
public class MagicWandWeapon : VS_BaseWeapon
{
    [SerializeField] protected float baseDamage = 15;
    [SerializeField] protected float baseCooldown = 1.5f;
    protected float cooldownTimer = 0;

    [SerializeField] List<CombatReceiver2D> enemiesOnTarget = new List<CombatReceiver2D>();
    [SerializeField] GameObject magicWandBulletPrefab;

    GameObject currentTarget = null;

    override public WeaponIdentifier WeaponId => WeaponIdentifier.MagicWandWeapon;

    public override void RunWeapon()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= baseCooldown * GetProjectileCooldown())
        {
            // Clean up null enemies
            enemiesOnTarget.RemoveAll(e => e == null);

            int projectileCount = GetProjectileAmount();

            // Get N closest targets
            List<CombatReceiver2D> targets = enemiesOnTarget
                .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
                .Take(projectileCount)
                .ToList();

            foreach (var target in targets)
            {
                GameObject newBullet = Instantiate(magicWandBulletPrefab, transform.position, Quaternion.identity);

                WeaponActor actor = newBullet.GetComponent<WeaponActor>();
                actor.SetTarget(target.gameObject);
                actor.Initialize(weaponStatModifiers, baseDamage);
            }

            cooldownTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var receiver = collision.gameObject.GetComponent<CombatReceiver2D>();
        if (receiver != null && receiver.GetFactionID() != 0)
        {
            enemiesOnTarget.Remove(receiver);
        }
    }
    //public override void RunWeapon()
    //{
    //    if (cooldownTimer < baseCooldown * GetProjectileCooldown())
    //    {
    //        cooldownTimer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        if (currentTarget != null)
    //        {
    //            GameObject newBullet = Instantiate(magicWandBulletPrefab, transform.position, Quaternion.identity);
    //            newBullet.GetComponent<WeaponActor>().SetTarget(currentTarget);

    //            MagicWandBullet newBulletActor = newBullet.GetComponent<MagicWandBullet>();
    //            newBulletActor.Initialize(weaponStatModifiers);

    //            cooldownTimer = 0;
    //        }
    //        else
    //        {
    //            foreach (CombatReceiver2D combatReceiver2D in enemiesOnTarget)
    //            {
    //                if (currentTarget == null)
    //                {
    //                    if (combatReceiver2D == null)
    //                    {
    //                        enemiesOnTarget.Remove(combatReceiver2D);
    //                        return;
    //                    }
    //                    currentTarget = combatReceiver2D.gameObject;
    //                }
    //                else
    //                {

    //                }
    //            }
    //        }
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CombatReceiver2D>() != null && collision.isTrigger)
        {
            if (collision.gameObject.GetComponent<CombatReceiver2D>().GetFactionID() != 0)
            {
                enemiesOnTarget.Add(collision.gameObject.GetComponent<CombatReceiver2D>());
            }
        }
    }
}
