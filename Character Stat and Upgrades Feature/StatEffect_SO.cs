using UnityEngine;

[CreateAssetMenu(fileName = "StatEffect_SO", menuName = "Scriptable Objects/StatEffect_SO")]
public class StatEffect_SO : ScriptableObject
{
    public CharacterStatType statType;
    public float multiplier = 1f;

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

    }

    public void ApplyToActor(WeaponActor actor, CharacterStats stats, float value)
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
        
        switch (statType)
        {
            case CharacterStatType.Area:
                actor.IncreaseAreaMultiplier(value);
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
