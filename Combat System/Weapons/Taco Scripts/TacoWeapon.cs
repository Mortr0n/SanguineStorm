using UnityEngine;

public class TacoWeapon : MagicWandWeapon
{
    public WeaponStatPreset_SO tacoPreset;
    public override WeaponIdentifier WeaponId => WeaponIdentifier.TacoWeapon;

    private int lastDirectionIndex = -1;

    public override void RunWeapon()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= GetProjectileCooldown())
        {
            FireTacos();
            cooldownTimer = 0f;
        }
    }

    public override void Initialize(WeaponStatModifiers modifiers, CharacterStats characterStats)
    {
        base.Initialize(modifiers, characterStats);

        
    }

    protected override void OnFirstInitialize()
    {
        if (tacoPreset != null)
        {
            weaponStatModifiers.maxPSpeed = tacoPreset.maxProjectileSpeed;
            weaponStatModifiers.minPSpeed = tacoPreset.minProjectileSpeed;
            weaponStatModifiers.maxArea = tacoPreset.maxProjectileArea;
            weaponStatModifiers.maxDuration = tacoPreset.maxProjectileDuration;
            weaponStatModifiers.maxMight = tacoPreset.maxProjectileMight;
            weaponStatModifiers.maxCooldown = tacoPreset.maxProjectileCooldown;
            weaponStatModifiers.minCooldown = tacoPreset.minProjectileCooldown;
            weaponStatModifiers.maxPAmount = tacoPreset.maxProjectileAmount;
            weaponStatModifiers.minPAmount = tacoPreset.minProjectileAmount;
        }
    }

    private void FireTacos()
    {
        int tacoCount = GetProjectileAmount(); // Max 12
        Vector2[] fireDirections = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

        for (int i = 0; i < tacoCount; i++)
        {
            Vector2 direction = fireDirections[i % fireDirections.Length]; // Loop after 4

            GameObject taco = ObjectPooler.Instance.SpawnFromPool("Taco", transform.position, Quaternion.identity);
            TacoActor actor = taco.GetComponent<TacoActor>();

            actor.SetFixedDirection(direction);
            actor.Initialize(weaponStatModifiers);

            taco.transform.position = transform.position;
            taco.transform.rotation = Quaternion.identity;
            taco.SetActive(true);
        }
    }



}
