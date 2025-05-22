using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class MagicWandWeapon : VS_BaseWeapon
{
    [SerializeField] float baseDamage = 10;
    [SerializeField] float baseCooldown = 2;
    float cooldownTimer = 0;

    [SerializeField] List<CombatReceiver2D> enemiesOnTarget = new List<CombatReceiver2D>();
    [SerializeField] GameObject magicWandBulletPrefab;

    GameObject currentTarget = null;


    public override void RunWeapon()
    {
        if (cooldownTimer < baseCooldown * VS_PlayerCharacterSheet.instance.Stats().cooldown)
        {
            cooldownTimer += Time.deltaTime;
        }
        else
        {
            if (currentTarget != null)
            {
                GameObject newBullet = Instantiate(magicWandBulletPrefab, transform.position, Quaternion.identity);
                newBullet.GetComponent<WeaponActor>().SetTarget(currentTarget);
                cooldownTimer = 0;
            }
            else
            {
                foreach (CombatReceiver2D combatReceiver2D in enemiesOnTarget)
                {
                    if (currentTarget == null)
                    {
                        if (combatReceiver2D == null)
                        {
                            enemiesOnTarget.Remove(combatReceiver2D);
                            return;
                        }
                        currentTarget = combatReceiver2D.gameObject;
                    }
                    else
                    {

                    }
                }
            }
        }
    }

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
