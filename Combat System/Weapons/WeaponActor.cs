using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WeaponActor : CombatActor2D
{
    protected GameObject target;
    protected WeaponStatModifiers _weaponStatModifiers;
    protected CharacterStats characterStats;

    private WeaponActorIdentifier wActorId;
    public virtual WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.none;

    float baseArea = 1;
    public float magicColliderMultiple = 1f; //TODO: fix this.  It's a magic number I was applying for the magicelectricball and may not actually be necessary.

    //public float speed = 3f;
    protected virtual void Awake()
    {
        characterStats = VS_PlayerCharacterSheet.instance.Stats();
    }
        

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
        float adjusted = baseArea * _weaponStatModifiers.projectileAreaMult;
        return  Math.Min(adjusted, _weaponStatModifiers.maxArea);
    }

    protected virtual float GetProjectileSpeed()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileSpeed.");
            return characterStats.projectileSpeed;
        }
        float adjusted = characterStats.projectileSpeed * _weaponStatModifiers.projectileSpeedMult;
        Debug.Log("GetProjectileSpeed called with projectileSpeed: " + characterStats.projectileSpeed + " and projectileSpeedMult: " + _weaponStatModifiers.projectileSpeedMult);
        return Math.Min(adjusted, _weaponStatModifiers.maxPSpeed);
    }

    protected virtual float GetProjectileDuration()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileDuration.");
            return characterStats.duration;
        }
        float adjusted = characterStats.duration * _weaponStatModifiers.projectileDurationMult;
        Debug.Log("GetProjectileDuration called with projectileDuration: " + characterStats.duration + " and projectileDurationMult: " + _weaponStatModifiers.projectileDurationMult);

        return Math.Min(adjusted, _weaponStatModifiers.maxDuration);
    }

    protected virtual float GetProjectileMight()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileMight.");
            return characterStats.might;
        }
        float adjusted = characterStats.might * _weaponStatModifiers.projectileMightMult;
        Debug.Log("GetProjectileMight called with projectileMight: " + characterStats.might + " and projectileMightMult: " + _weaponStatModifiers.projectileMightMult);
        return Math.Min(adjusted, _weaponStatModifiers.maxMight);
    }

    protected virtual float GetProjectileCooldown()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileCooldown.");
            return characterStats.cooldown;
        }
        float adjusted = characterStats.cooldown * _weaponStatModifiers.projectileCooldownMult ;
        Debug.Log("GetProjectileCooldown called with projectileCooldown: " + characterStats.cooldown + " and projectileCooldownMult: " + _weaponStatModifiers.projectileCooldownMult);
        return Math.Min(adjusted, _weaponStatModifiers.maxCooldown);
    }

    protected virtual float GetProjectileAmount()
    {
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling GetProjectileAmount.");
            return characterStats.amount;
        }
        float adjusted = characterStats.amount * _weaponStatModifiers.projectileAmountMult;
        Debug.Log("GetProjectileAmount called with projectileAmount: " + characterStats.amount + " and projectileAmountMult: " + _weaponStatModifiers.projectileAmountMult);
        return Math.Min(adjusted, _weaponStatModifiers.projectileAmountMult);
    }
    
}
