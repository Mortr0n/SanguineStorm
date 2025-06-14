using UnityEngine;

public class TacoWeapon : MagicWandWeapon
{
    public WeaponStatPreset_SO tacoPreset;
    public override WeaponIdentifier WeaponId => WeaponIdentifier.TacoWeapon;

    private int lastDirectionIndex = -1;
    

    public override void RunWeapon()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= GetProjectileCooldown() * baseCooldown)
        {
            FireTacos();
            cooldownTimer = 0f;
        }
    }

    public override void Initialize(WeaponStatModifiers modifiers, CharacterStats characterStats)
    {
        Debug.Log($"Initializing TacoWeapon with modifiers and character stats... mods: {modifiers} and stats: {characterStats}");
        base.Initialize(modifiers, characterStats);
        //baseCooldown = 1f; // Reset base cooldown to 1 second for TacoWeapon
    }

    protected override void OnFirstInitialize()
    {
        baseDamage = .0005f; // Set base damage to a very low value for TacoWeapon
        Debug.Log("TacoWeapon first initialization...");
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
        if (tacoCount <= 0) Debug.Log("TacoWeapon: Taco count is zero or negative, no tacos will be fired.");
        //Debug.Log($"Taco count: {tacoCount}, Position: {transform.position}, Direction loop length: {fireDirections.Length}");

        for (int i = 0; i < tacoCount; i++)
        {
            Vector2 direction = fireDirections[i % fireDirections.Length]; // Loop after 4

            GameObject taco = ObjectPooler.Instance.SpawnFromPool("Taco", transform.position, Quaternion.identity);
            if (taco == null)
            {
                Debug.LogError("TacoWeapon: Failed to spawn Taco from pool. Check if the pool is set up correctly.");
                continue;
            }
            TacoActor actor = taco.GetComponent<TacoActor>();
            if (actor == null)
            {
                Debug.LogError("TacoWeapon: TacoActor component not found on Taco prefab.");
                continue;
            }

            actor.Initialize(weaponStatModifiers, baseDamage);
            actor.SetFixedDirection(direction);
            

            taco.transform.position = transform.position;
            taco.transform.rotation = Quaternion.identity;
            taco.SetActive(true);
        }
    }



}
