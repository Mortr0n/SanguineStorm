using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MagicElectricBallWeapon : VS_BaseWeapon
{
    [SerializeField] private List<GameObject> enemyTargetOptions = new();
    [SerializeField] GameObject magicBallPrefab;
    
    [SerializeField] float baseDamage = 10;
    [SerializeField] float baseCooldown = 3;
    
    [SerializeField] float minCastCooldown = 1f;
    float cooldownTimer = 0;

    GameObject currentTarget = null;

    float spreadAngle = 30f; // Angle between projectiles when firing multiple

    override public WeaponIdentifier WeaponId => WeaponIdentifier.MagicElectricBallWeapon;
    private void Start()
    {
        weaponStatModifiers.projectileDurationMult = 1.5f;
    }

    public override void RunWeapon()
    {
        cooldownTimer += Time.deltaTime;

        float adjustedCooldown = Mathf.Max(baseCooldown * VS_PlayerCharacterSheet.instance.Stats().cooldown *weaponStatModifiers.projectileCooldownMult, minCastCooldown);

        if (cooldownTimer >= adjustedCooldown)
        {
            enemyTargetOptions.RemoveAll(go => go == null || !go.activeInHierarchy);

            currentTarget = GetClosestTarget(enemyTargetOptions);

            if (currentTarget != null)
            {
                int projectileCount = GetProjectileAmount();
                float totalSpread = spreadAngle * (projectileCount - 1);
                float startAngle = -totalSpread / 2f;

                Vector3 baseDirection = (currentTarget.transform.position - transform.position).normalized;
                for (int i = 0; i < projectileCount; i++)
                {
                    float angle = startAngle + spreadAngle * i;
                    Vector3 rotatedDirection = Quaternion.Euler(0, 0, angle) * baseDirection;

                    GameObject projectile = Instantiate(magicBallPrefab, transform.position, Quaternion.identity);
                    MagicElectricBallActor magicActor = projectile.GetComponent<MagicElectricBallActor>();

                    magicActor.SetDirection(rotatedDirection);

                    bool isCenterProjectile = (i == projectileCount / 2);
                    if (isCenterProjectile) magicActor.SetTarget(currentTarget);
                    else magicActor.SetFixedDirection(rotatedDirection);

                    magicActor.Initialize(weaponStatModifiers);
                }

                

                

                cooldownTimer = 0f;
            }
            else
            {
                cooldownTimer = adjustedCooldown; // wait for valid targets
            }
        }
    }

    // TODO: add projectile amount firing options maybe to base weapon that picks multiple targets and fires multiple projectiles at once if the amount changes

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CombatReceiver2D>() == null) return;

        if (other.gameObject.GetComponent<CombatReceiver2D>().GetFactionID() == 1)
        {
            enemyTargetOptions.Add(other.gameObject);
        }
    }

}
