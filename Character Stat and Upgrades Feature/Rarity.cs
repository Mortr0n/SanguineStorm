using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public class Rarity
{
    public enum RarityType
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public RarityType rarityType;
    public Color GetRarityColor()
    {
        switch (rarityType)
        {
            case RarityType.Common:
                return new Color(197f / 255f, 122f / 255f, 122f / 255f, 1f); // Light brownish
            case RarityType.Uncommon:
                return new Color(197f / 255f, 218f / 255f, 122f / 255f, 1f); // Greenish
            case RarityType.Rare:
                return new Color(75f / 255f, 109f / 255f, 147f / 255f, 1f);  // Blue
            case RarityType.Epic:
                return new Color(197f / 255f, 99f / 255f, 207f / 255f, 1f);  // Purple
            case RarityType.Legendary:
                return new Color(229f / 255f, 187f / 255f, 60f / 255f, 1f);  // Gold
            default:
                return new Color(197f / 255f, 122f / 255f, 122f / 255f, 1f); // Default to common
        }
    }

    public static RarityType GetRandomRarity()
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < 50)
            return RarityType.Common; // 50% chance for Common
        else if (randomValue < 80)
            return RarityType.Uncommon; // 30% chance for Uncommon
        else if (randomValue < 95)
            return RarityType.Rare; // 15% chance for Rare
        else if (randomValue >= 95)
            return RarityType.Epic; // 5% chance for Epic
        else
            return (RarityType)randomValue;
    }
}
