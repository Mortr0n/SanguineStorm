using UnityEngine;

public class StatEffectScaling
{
    public static float GetRarityEffectValue(CharacterStatType stat, Rarity.RarityType rarity)
    {
        switch (stat)
        {
            case CharacterStatType.Area:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.Might:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.2f,
                    Rarity.RarityType.Uncommon => 0.4f,
                    Rarity.RarityType.Rare => 0.5f,
                    Rarity.RarityType.Epic => 0.75f,
                    Rarity.RarityType.Legendary => 1.0f,
                    _ => 0.2f
                };
            case CharacterStatType.ProjectileSpeed:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.Duration:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.Cooldown:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.ProjectileAmount:
                return rarity switch
                {
                    Rarity.RarityType.Common => 1f,
                    Rarity.RarityType.Uncommon => 1f,
                    Rarity.RarityType.Rare => 2f,
                    Rarity.RarityType.Epic => 2f,
                    Rarity.RarityType.Legendary => 3f,
                    _ => 1f
                };
            case CharacterStatType.Magnet:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.MaxHealth:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.Recovery:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.Armor:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };
            case CharacterStatType.Speed:
                return rarity switch
                {
                    Rarity.RarityType.Common => 0.1f,
                    Rarity.RarityType.Uncommon => 0.2f,
                    Rarity.RarityType.Rare => 0.3f,
                    Rarity.RarityType.Epic => 0.4f,
                    Rarity.RarityType.Legendary => 0.5f,
                    _ => 0.1f
                };

            //TODO: Add others...
            default: return 0.1f;
        }

    }
}
