using System;
using UnityEngine;

public class WeaponActor : CombatActor2D
{
    protected GameObject target;
    [SerializeField] protected float projectileSpeedMult = 1f;
    [SerializeField] protected float maxPSpeed = 10f;
    [SerializeField] protected float minPSpeed = 0.5f;

    [SerializeField] protected float projectileAreaMult = 1f;
    [SerializeField] protected float maxArea = 10f;

    [SerializeField] protected float projectileDurationMult = 1f;
    [SerializeField] protected float maxDuration = 10f;


    [SerializeField] protected float projectileMightMult = 1f;
    [SerializeField] protected float maxMight = 10f;


    [SerializeField] protected float projectileCooldownMult = 1f;
    [SerializeField] protected float maxCooldown = 10f;
    [SerializeField] protected float minCooldown = 0.1f;

    [SerializeField] protected float projectileAmountMult = 1f;
    [SerializeField] protected float maxPAmount = 10f;
    [SerializeField] protected float minPAmount = 1f;


    [SerializeField] protected float projectileTimeToDestroyMult = 3f;
    [SerializeField] protected float maxTimeToDestroy = 10f;

    [SerializeField] protected float projectileDamageMult = 1f;

    private WeaponActorIdentifier wActorId;
    public virtual WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.none;


    public virtual void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }


    public virtual void IncreaseAreaMultiplier(float value)
    {
        projectileAreaMult += value;        
    }

    protected virtual float GetAttackArea(float attackArea)
    {
        return  attackArea = Math.Min(attackArea * projectileAreaMult, maxArea);
    }
}
