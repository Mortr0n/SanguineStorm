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
    public float maxMight = 10000f;


    public float projectileCooldownMult = 1f;
    public float maxCooldown = 10f;
    public float minCooldown = 0.1f;

    public int projectileAmountMult = 1;
    public int maxPAmount = 10;
    public int minPAmount = 1;


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
                projectileAmountMult = Mathf.Clamp(projectileAmountMult + (int)value, minPAmount, maxPAmount);
                Debug.Log($"Projectile Amount Multiplier: {projectileAmountMult} and val {value}");
                break;

        }
    }

    public WeaponStatModifiers Clone()
    {
        return new WeaponStatModifiers
        {
            projectileSpeedMult = this.projectileSpeedMult,
            maxPSpeed = this.maxPSpeed,
            minPSpeed = this.minPSpeed,
            projectileAreaMult = this.projectileAreaMult,
            maxArea = this.maxArea,
            projectileDurationMult = this.projectileDurationMult,
            maxDuration = this.maxDuration,
            projectileMightMult = this.projectileMightMult,
            maxMight = this.maxMight,
            projectileCooldownMult = this.projectileCooldownMult,
            maxCooldown = this.maxCooldown,
            minCooldown = this.minCooldown,
            projectileAmountMult = this.projectileAmountMult,
            maxPAmount = this.maxPAmount,
            minPAmount = this.minPAmount,
            projectileDamageMult = this.projectileDamageMult,
        };
    }
}
