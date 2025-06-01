using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeCard_SO", menuName = "Upgrade/UpgradeCard_SO")]
public class UpgradeCard_SO : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    [SerializeField] public Rarity rarity;

    public StatEffect_SO effect;
    //public float effectValue;


    public WeaponIdentifier weaponIdentifier; // enum: none, MagicElectricBall, MagicWand, FireBomb, TacoWeapon, GarlicWeapon
    public WeaponActorIdentifier weaponActorIdentifier; // optional, used for Actor target
    public StatEffectTarget target; // enum: Player, Weapon, Actor
    public bool isWeaponEquipCard; // true if this card is for equipping a weapon, false if it's for applying an upgrade effect

    public void Apply()
    {
        CharacterStats stats = VS_PlayerCharacterSheet.instance.Stats();
        VS_PlayerController playerController = VS_PlayerController.instance;

        if (isWeaponEquipCard)
        {
            playerController.EquipWeaponById(weaponIdentifier);
            return;
        }

        Debug.Log("Applying upgrade: " + upgradeName);
        switch (target)
        {
            case StatEffectTarget.Player:
                //effect.ApplyToPlayer(stats, effectValue);
                break;

            case StatEffectTarget.Weapon:
                var weapons = Object.FindObjectsByType<VS_BaseWeapon>(FindObjectsSortMode.None);
                foreach (var weapon in weapons)
                {
                    Debug.Log($"Checking weapon: {weapon.name} with ID: {weapon.WeaponId} against {weaponIdentifier}");
                    if (weapon.WeaponId == weaponIdentifier)
                    {
                        Debug.Log($"Applying {effect.name} to weapon: {weapon.name}");
                        float rarityAdjustedValue = StatEffectScaling.GetRarityEffectValue(effect.statType, rarity.rarityType);
                        effect.ApplyToWeapon(weapon, stats, rarityAdjustedValue);
                        return;
                    }
                }
                break;
        }
    }
}
