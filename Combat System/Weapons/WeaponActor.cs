using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WeaponActor : CombatActor2D
{
    protected GameObject target;
    protected WeaponStatModifiers _weaponStatModifiers;
    protected CharacterStats characterStats = VS_PlayerCharacterSheet.instance.Stats();

    private WeaponActorIdentifier wActorId;
    public virtual WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.none;

    float baseArea = 1;
    public float magicColliderMultiple = 1f; //TODO: fix this.  It's a magic number I was applying for the magicelectricball and may not actually be necessary.


    public virtual void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public virtual void SetAttackArea()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        var area = GetAttackArea();
        transform.localScale *= area;
        collider.radius *= area * magicColliderMultiple; // may need to change
    }

    protected virtual float GetAttackArea()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetAttackArea.");
            return baseArea;
        }
        Debug.Log("GetAttackArea called with attackArea: " + baseArea + " and projectileAreaMult: " + _weaponStatModifiers.projectileAreaMult);
        return  Math.Min(baseArea * _weaponStatModifiers.projectileAreaMult, _weaponStatModifiers.maxArea);
    }

    protected virtual float GetProjectileSpeed()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileSpeed.");
            return characterStats.projectileSpeed;
        }
        Debug.Log("GetProjectileSpeed called with projectileSpeed: " + characterStats.projectileSpeed + " and projectileSpeedMult: " + _weaponStatModifiers.projectileSpeedMult);
        return Math.Min(characterStats.projectileSpeed *= (_weaponStatModifiers.projectileSpeedMult * baseSpeed), _weaponStatModifiers.maxPSpeed);
    }

    protected virtual float GetProjectileDuration()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileDuration.");
            return characterStats.duration;
        }
        Debug.Log("GetProjectileDuration called with projectileDuration: " + characterStats.duration + " and projectileDurationMult: " + _weaponStatModifiers.projectileDurationMult);
        return Math.Min(characterStats.duration *= (_weaponStatModifiers.projectileDurationMult + 1), _weaponStatModifiers.maxDuration);
    }

    protected virtual float GetProjectileMight()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileMight.");
            return characterStats.might;
        }
        Debug.Log("GetProjectileMight called with projectileMight: " + characterStats.might + " and projectileMightMult: " + _weaponStatModifiers.projectileMightMult);
        return Math.Min(characterStats.might *= (_weaponStatModifiers.projectileMightMult + 1), _weaponStatModifiers.maxMight);
    }

    protected virtual float GetProjectileCooldown()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileCooldown.");
            return characterStats.cooldown;
        }
        Debug.Log("GetProjectileCooldown called with projectileCooldown: " + characterStats.cooldown + " and projectileCooldownMult: " + _weaponStatModifiers.projectileCooldownMult);
        return Math.Min(characterStats.cooldown *= (_weaponStatModifiers.projectileCooldownMult + 1), _weaponStatModifiers.maxCooldown);
    }

    protected virtual float GetProjectileAmount()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileAmount.");
            return characterStats.amount;
        }
        Debug.Log("GetProjectileAmount called with projectileAmount: " + characterStats.amount + " and projectileAmountMult: " + _weaponStatModifiers.projectileAmountMult);
        return Math.Min(characterStats.amount *= (int)(_weaponStatModifiers.projectileAmountMult + 1), _weaponStatModifiers.projectileAmountMult);
    }
    
}
