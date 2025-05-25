using UnityEngine;

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
                return Color.white;
            case RarityType.Uncommon:
                return Color.green;
            case RarityType.Rare:
                return Color.blue;
            case RarityType.Epic:
                return new Color(0.5f, 0, 0.5f); // Purple
            case RarityType.Legendary:
                return Color.yellow;
            default:
                return Color.white;
        }
    }

}
