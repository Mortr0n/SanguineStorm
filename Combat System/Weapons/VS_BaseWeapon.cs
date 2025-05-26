using UnityEngine;
using System.Collections.Generic;
using System;

public class VS_BaseWeapon : MonoBehaviour
{
    private WeaponIdentifier weaponId;
    public WeaponStatModifiers weaponStatModifiers = new WeaponStatModifiers();
    public virtual WeaponIdentifier WeaponId => WeaponIdentifier.none;
    // weapon is going to need all the actor variables so it can store them to pass on the changes at initialization so we'll
    // update the weapon for the upgrades, then we'll initialize with the weapon calling their init with any vars.  we could
    // even do a single object for all the vars that we pass in or something because there's a bunch so it should proabably just pass in 
    // the entire object of the multiplier vars at initialization.

    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public virtual void RunWeapon()
    {

    }

    public GameObject GetClosestTarget(List<GameObject> enemiesInRange)
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


