using UnityEngine;

public class GarlicWeapon : VS_BaseWeapon
{
    [SerializeField] GameObject garlicPrefab;
    float garlicSpawnTime = 3f;
    float garlicSpawnTimer = 0f;

    public override WeaponIdentifier WeaponId => WeaponIdentifier.GarlicWeapon;

    public override void RunWeapon()
    {
        garlicSpawnTimer += Time.deltaTime;

        float adjustedCooldown = Mathf.Max(garlicSpawnTime * weaponStatModifiers.projectileCooldownMult, 0.1f); // prevent 0

        if (garlicSpawnTimer >= adjustedCooldown)
        {
            GameObject garlic = Instantiate(garlicPrefab, transform.position, Quaternion.identity, transform);
            GarlicCombatActor actor = garlic.GetComponent<GarlicCombatActor>();
            actor.Initialize(weaponStatModifiers, .005f);
            garlicSpawnTimer = 0f;
        }
    }

    //public override void RunWeapon()
    //{
    //    garlicSpawnTimer += Time.deltaTime;
    //    Debug.Log($"Garlic prefab : {garlicPrefab}");
    //    if (garlicSpawnTimer > garlicSpawnTime)
    //    {
    //        GameObject garlic = Instantiate(garlicPrefab, transform);
    //        Debug.Log($"garlic : {garlic}");
    //        garlicSpawnTimer -= garlicSpawnTime;
    //    }
    //}
}
