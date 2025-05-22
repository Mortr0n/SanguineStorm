using UnityEngine;

public class WeaponActor : CombatActor2D
{
    protected GameObject target;

    public virtual void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}
