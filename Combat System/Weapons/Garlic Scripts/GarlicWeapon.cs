using UnityEngine;

public class GarlicWeapon : VS_BaseWeapon
{
    [SerializeField] GameObject garlicPrefab;
    float garlicSpawnTime = 1f;
    float garlicSpawnTimer = 0f;
    public override void RunWeapon()
    {
        garlicSpawnTimer += Time.deltaTime;
        Debug.Log($"Garlic prefab : {garlicPrefab}");
        if (garlicSpawnTimer > garlicSpawnTime)
        {
            GameObject garlic = Instantiate(garlicPrefab, transform);
            Debug.Log($"garlic : {garlic}");
            garlicSpawnTimer -= garlicSpawnTime;
        }
    }
}
