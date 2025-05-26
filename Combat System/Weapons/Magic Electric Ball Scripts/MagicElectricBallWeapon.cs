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
    
    override public WeaponIdentifier WeaponId => WeaponIdentifier.MagicElectricBallWeapon;
    private void Start()
    {
        weaponStatModifiers.projectileDurationMult = 1.5f;
    }

    public override void RunWeapon()
    {
        cooldownTimer += Time.deltaTime;

        float adjustedCooldown = Mathf.Max(baseCooldown * VS_PlayerCharacterSheet.instance.Stats().cooldown, minCastCooldown);

        if (cooldownTimer >= adjustedCooldown)
        {
            enemyTargetOptions.RemoveAll(go => go == null || !go.activeInHierarchy);

            currentTarget = GetClosestTarget(enemyTargetOptions);

            if (currentTarget != null)
            {
                GameObject projectile = Instantiate(magicBallPrefab, transform.position, Quaternion.identity);
                MagicElectricBallActor magicActor = projectile.GetComponent<MagicElectricBallActor>();

                magicActor.SetTarget(currentTarget);
                magicActor.Initialize(weaponStatModifiers);

                cooldownTimer = 0f;
            }
            else
            {
                cooldownTimer = adjustedCooldown; // wait for valid targets
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CombatReceiver2D>() == null) return;

        if (other.gameObject.GetComponent<CombatReceiver2D>().GetFactionID() == 1)
        {
            enemyTargetOptions.Add(other.gameObject);
        }
    }

}
