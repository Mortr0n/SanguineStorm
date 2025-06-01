using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatPreset_SO", menuName = "Weapon/WeaponStatPreset_SO")]
public class WeaponStatPreset_SO : ScriptableObject
{
    public int maxProjectileSpeed = 10;
    public int minProjectileSpeed = 1;
    public int maxProjectileArea = 10;
    public int maxProjectileDuration = 10;
    public int maxProjectileMight = 10000;
    public int maxProjectileCooldown = 10;
    public int minProjectileCooldown = 1;
    public int maxProjectileAmount = 10;
    public int minProjectileAmount = 1;
}
