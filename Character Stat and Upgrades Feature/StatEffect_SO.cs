using UnityEngine;

[CreateAssetMenu(fileName = "StatEffect_SO", menuName = "Scriptable Objects/StatEffect_SO")]
public class StatEffect_SO : ScriptableObject
{
    public CharacterStatType statType;
    public float multiplier = 1f;
    public float minValue = 0f;
    public float maxValue = 100f;

    public void ApplyToWeapon(VS_BaseWeapon weapon, CharacterStats stats)
    {
        if (weapon == null)
        {
            Debug.LogError("Weapon is null");
            return;
        }
        if (stats == null)
        {
            Debug.LogError("Stats is null");
            return;
        }

        float value = stats.GetStat(statType) * multiplier;
        value = Mathf.Max(value, minValue);
        value = Mathf.Min(value, maxValue);

        switch (statType)
        {
            case CharacterStatType.Cooldown:
                weapon.SetCooldown = value; 
                break;
            case CharacterStatType.ProjectileAmount:
                weapon.SetProjectileAmount = value;
                break;
        }
    }

    public void ApplyToActor(WeaponActor actor, CharacterStats stats)
    {
        if (actor == null)
        {
            Debug.LogError("Actor is null");
            return;
        }
        if (stats == null)
        {
            Debug.LogError("Stats is null");
            return;
        }

        float value = stats.GetStat(statType) * multiplier;
        value = Mathf.Max(value, minValue);
        value = Mathf.Min(value, maxValue);
        
        switch (statType)
        {
            case CharacterStatType.Area:
                actor.SetArea = value;
                break;
            case CharacterStatType.ProjectileSpeed:
                actor.SetProjectileSpeed = value;
                break;
        }
    }

    public void ApplyToPlayer(CharacterStats stats)
    {
        switch (statType)
        {
            case CharacterStatType.MaxHealth:
                stats.maxHealth *= stats.maxHealth * multiplier;
                break;
            case CharacterStatType.Recovery:
                stats.recovery *= multiplier;
                break;
        }
    }
}
