using UnityEngine;

[CreateAssetMenu(fileName = "StatEffect_SO", menuName = "Scriptable Objects/StatEffect_SO")]
public class StatEffect_SO : ScriptableObject
{
    public CharacterStatType statType;
    //public float multiplier = 1f; // TODO: removing for after setting up rarity effect like on the apply to weapon and then pass in from upgrade card

    public void ApplyToWeapon(VS_BaseWeapon weapon, CharacterStats stats, float value)
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

        weapon.weaponStatModifiers.ApplyStat(statType, value);
    }


    public void ApplyToPlayer(CharacterStats stats, float value)
    {
        switch (statType)
        {
            case CharacterStatType.MaxHealth:
                stats.maxHealth *= stats.maxHealth * value;
                break;
            case CharacterStatType.Recovery:
                stats.recovery *= value;
                break;
        }
    }
}
