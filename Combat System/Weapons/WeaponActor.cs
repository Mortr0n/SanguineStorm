using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class WeaponActor : CombatActor2D
{
    protected GameObject target;
    protected Vector2? fixedDirection = null; // If we want to set a fixed direction for the weapon, otherwise it will use the target's position to determine direction.
    protected Vector2 moveDirection;
    protected WeaponStatModifiers _weaponStatModifiers;
    protected CharacterStats characterStats;

    private WeaponActorIdentifier wActorId;
    public virtual WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.none;

    float baseArea = 1;
    public float magicColliderMultiple = 1f; //TODO: fix this.  It's a magic number I was applying for the magicelectricball and may not actually be necessary.

    float projectileSpeed;
    float projectileDuration;
    float projectileMight;
    float projectileCooldown;
    float projectileAmount;

    protected bool useFixedDirection = false;

    protected virtual void Awake()
    {
        characterStats = VS_PlayerCharacterSheet.instance.Stats();
    }

    protected virtual void Update()
    {

    }

    public virtual void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public virtual void SetFixedDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            Debug.LogWarning("WeaponActor: SetFixedDirection called with zero vector. Please provide a valid direction.");
            return;
        }
        fixedDirection = dir;
        moveDirection = dir.normalized;
        useFixedDirection = true;

    }

    public virtual void SetDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            Debug.LogWarning("WeaponActor: SetDirection called with zero vector. Please provide a valid direction.");
            return;
        }
        transform.up = direction; // Assuming the weapon's forward direction is up
    }

    public virtual void Initialize(WeaponStatModifiers weaponStatModifiers)
    {
        _weaponStatModifiers = weaponStatModifiers;
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("WeaponActor: weaponStatModifiers is null. Please initialize it before calling Initialize.");
            return;
        }
        damage = GetProjectileMight();

            SetAttackArea();
        projectileSpeed = GetProjectileSpeed();
        projectileDuration = GetProjectileDuration();
        projectileMight = GetProjectileMight();
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

    

}
