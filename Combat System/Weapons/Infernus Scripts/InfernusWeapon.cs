using UnityEngine;

public class InfernusWeapon : MagicWandWeapon
{
    public WeaponStatPreset_SO infernusPreset;
    public override WeaponIdentifier WeaponId => WeaponIdentifier.InfernusWeapon;

    private int lastDirectionIndex = -1;
    

    public override void RunWeapon()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= GetProjectileCooldown() * baseCooldown)
        {
            FireInfernus();
            cooldownTimer = 0f;
        }
    }

    public override void Initialize(WeaponStatModifiers modifiers, CharacterStats characterStats)
    {
        Debug.Log($"Initializing InfernusWeapon with modifiers and character stats... mods: {modifiers} and stats: {characterStats}");
        base.Initialize(modifiers, characterStats);
        //baseCooldown = 1f; // Reset base cooldown to 1 second for InfernusWeapon
    }

    protected override void OnFirstInitialize()
    {
        baseDamage = .0005f; // Set base damage to a very low value for InfernusWeapon
        Debug.Log("InfernusWeapon first initialization...");
        if (infernusPreset != null)
        {
            weaponStatModifiers.maxPSpeed = infernusPreset.maxProjectileSpeed;
            weaponStatModifiers.minPSpeed = infernusPreset.minProjectileSpeed;
            weaponStatModifiers.maxArea = infernusPreset.maxProjectileArea;
            weaponStatModifiers.maxDuration = infernusPreset.maxProjectileDuration;
            weaponStatModifiers.maxMight = infernusPreset.maxProjectileMight;
            weaponStatModifiers.maxCooldown = infernusPreset.maxProjectileCooldown;
            weaponStatModifiers.minCooldown = infernusPreset.minProjectileCooldown;
            weaponStatModifiers.maxPAmount = infernusPreset.maxProjectileAmount;
            weaponStatModifiers.minPAmount = infernusPreset.minProjectileAmount;
        }
    }

    private void FireInfernus()
    {
        int infernusCount = GetProjectileAmount(); // Max 12
        Vector2[] fireDirections = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };
        if (infernusCount <= 0) Debug.Log("InfernusWeapon: Infernus count is zero or negative, no infernus will be fired.");
        //Debug.Log($"Infernus count: {infernusCount}, Position: {transform.position}, Direction loop length: {fireDirections.Length}");

        for (int i = 0; i < infernusCount; i++)
        {
            Vector2 direction = fireDirections[i % fireDirections.Length]; // Loop after 4

            GameObject infernus = ObjectPooler.Instance.SpawnFromPool("Infernus", transform.position, Quaternion.identity);
            if (infernus == null)
            {
                Debug.LogError("InfernusWeapon: Failed to spawn Infernus from pool. Check if the pool is set up correctly.");
                continue;
            }
            InfernusActor actor = infernus.GetComponent<InfernusActor>();
            if (actor == null)
            {
                Debug.LogError("InfernusWeapon: InfernusActor component not found on Infernus prefab.");
                continue;
            }

            actor.Initialize(weaponStatModifiers, baseDamage);
            actor.SetFixedDirection(direction);
            

            infernus.transform.position = transform.position;
            infernus.transform.rotation = Quaternion.identity;
            infernus.SetActive(true);
        }
    }



}
}
