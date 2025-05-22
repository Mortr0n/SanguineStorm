using UnityEngine;

public class WeaponStatEffectRunner : MonoBehaviour
{
    [SerializeField] private VS_BaseWeapon weapon;
    [SerializeField] private StatEffect_SO[] effects;

    void Start()
    {
        if (weapon == null)
        {
            GetComponent<VS_BaseWeapon>();
        }
        var stats = VS_PlayerCharacterSheet.instance.GetComponent<CharacterStats>();

        foreach (var effect in effects)
        {
            effect.ApplyToWeapon(weapon, stats);
        }
    }

}
