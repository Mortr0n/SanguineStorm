using UnityEngine;
using System.Collections.Generic;
using System;

public class VS_BaseWeapon : MonoBehaviour
{
    private WeaponIdentifier weaponId;
    public WeaponStatModifiers weaponStatModifiers = new WeaponStatModifiers();
    public CharacterStats characterStats = new CharacterStats();
    public virtual WeaponIdentifier WeaponId => WeaponIdentifier.none;

    private bool isInitialized = false;
    // weapon is going to need all the actor variables so it can store them to pass on the changes at initialization so we'll
    // update the weapon for the upgrades, then we'll initialize with the weapon calling their init with any vars.  we could
    // even do a single object for all the vars that we pass in or something because there's a bunch so it should proabably just pass in 
    // the entire object of the multiplier vars at initialization.
    private int projectileAmount;
    private float cooldown;

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }
    void Start()
    {
        //startingRadius = GetComponent<CircleCollider2D>().radius;
    }
    public virtual void Initialize(WeaponStatModifiers modifiers, CharacterStats characterStats)
    {
        //if (isInitialized) return;

        this.weaponStatModifiers = modifiers;
        this.characterStats = characterStats;
        if (!isInitialized)
        {
            OnFirstInitialize();
            isInitialized = true;
        }
    }

    protected virtual void OnFirstInitialize()
    {
        // Override this method in derived classes to perform additional initialization
        Debug.Log("Base weapon initialized with modifiers: " + weaponStatModifiers);
    }

    protected virtual float GetProjectileCooldown()
    {
        if (weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileCooldown.");
            return characterStats.cooldown;
        }
        float adjusted = characterStats.cooldown * weaponStatModifiers.projectileCooldownMult;
        Debug.Log("GetProjectileCooldown called with projectileCooldown: " + characterStats.cooldown + " and projectileCooldownMult: " + weaponStatModifiers.projectileCooldownMult);
        return Math.Min(adjusted, weaponStatModifiers.maxCooldown);
    }

    protected virtual int GetProjectileAmount()
    {
        if (weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileAmount.");
            return characterStats.amount;
        }
        int adjusted = characterStats.amount * weaponStatModifiers.projectileAmountMult;

        Debug.Log("GetProjectileAmount called with projectileAmount: " + characterStats.amount + " and projectileAmountMult: " + weaponStatModifiers.projectileAmountMult);
        Debug.Log($"Adjusted Projectile Count: {adjusted}, Max Allowed: {weaponStatModifiers.maxPAmount}, char: {characterStats.amount} pamtmult {weaponStatModifiers.projectileAmountMult}");

        return Mathf.Clamp(adjusted, weaponStatModifiers.minPAmount, weaponStatModifiers.maxPAmount);
    }


    public virtual void RunWeapon()
    {

    }

    public virtual GameObject GetClosestTarget(List<GameObject> enemiesInRange)
    {
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, currentPosition);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                return enemy;
                
            }
        }
        return null;
    }



    //public virtual void IncreaseAreaMultiplier(float value)
    //{
    //    weaponStatModifiers.projectileAreaMult += value;
    //}

    //protected virtual float GetAttackArea(float attackArea)
    //{
    //    return attackArea =  Math.Min(attackArea * weaponStatModifiers.projectileAreaMult, weaponStatModifiers.maxArea);
    //}


}


