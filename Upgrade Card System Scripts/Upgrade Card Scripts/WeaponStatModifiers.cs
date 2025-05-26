using UnityEngine;

public class WeaponStatModifiers
{
    public float projectileSpeedMult = 1f;
    public float maxPSpeed = 10f;
    public float minPSpeed = 0.5f;

    public float projectileAreaMult = 1f;
    public float maxArea = 10f;

    public float projectileDurationMult = 1f;
    public float maxDuration = 10f;


    public float projectileMightMult = 1f;
    public float maxMight = 10f;


    public float projectileCooldownMult = 1f;
    public float maxCooldown = 10f;
    public float minCooldown = 0.1f;

    public float projectileAmountMult = 1f;
    public float maxPAmount = 10f;
    public float minPAmount = 1f;


    // Redundant????
    //public float projectileTimeToDestroyMult = 3f;
    //public float maxTimeToDestroy = 10f;

    public float projectileDamageMult = 0f;

    public void ApplyStat(CharacterStatType stat, float value)
    {
        switch(stat)
        {
            case CharacterStatType.Area:
                projectileAreaMult = Mathf.Clamp(projectileAreaMult + value, 1f, maxArea);
                break;
            case CharacterStatType.ProjectileSpeed:            
                projectileSpeedMult = Mathf.Clamp(projectileSpeedMult + value, minPSpeed, maxPSpeed);
                break;
            case CharacterStatType.Duration:
                projectileDurationMult = Mathf.Clamp(projectileDurationMult + value, 1f, maxDuration);
                break;
            case CharacterStatType.Might:
                projectileMightMult = Mathf.Clamp(projectileMightMult + value, 1f, maxMight);
                break;
            case CharacterStatType.Cooldown:
                projectileCooldownMult = Mathf.Clamp(projectileCooldownMult  + value, minCooldown, maxCooldown);
                break;
            case CharacterStatType.ProjectileAmount:
                projectileAmountMult = Mathf.Clamp(projectileAmountMult + value, minPAmount, maxPAmount);
                break;

        }
    }
}
