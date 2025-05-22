using UnityEngine;

public class VS_RandomLootEnemy : VS_BasicEnemyController
{
    [SerializeField] protected GameObject alternativeLootPrefab; // Prefab for the random loot item
    [SerializeField] protected GameObject goldLootPrefab; // Prefab for the gold loot item
    protected override void DropLoot()
    {
        // Randomly Drop 1 of 2 loot items
        int randomLootRoll = Random.Range(0, 100);
        int randomLoot;

        // TODO: Modify as necessary for alternative loot
        if (randomLootRoll < 10) randomLoot = 1;
        else randomLoot = 0;

        switch (randomLoot)
        {
            case 0:  // XP Loot
                //TODO: get audio manager working
                //AudioManager.instance.PlayPickupXPSFX();
                if (lootPrefab != null) Instantiate(lootPrefab, transform.position, Quaternion.identity);
                else Debug.LogWarning("No loot prefab found for random loot drop rolling XP roll");
                break;
            case 1: // Gold Loot
                //TODO: get audio manager working
                //AudioManager.instance.PlayPickupGoldSFX();
                if (goldLootPrefab != null) Instantiate(goldLootPrefab, transform.position, Quaternion.identity);
                // if gold Prefab is empty try xp
                else if (lootPrefab != null) Instantiate(lootPrefab, transform.position, Quaternion.identity);
                else Debug.LogWarning("No loot prefab found for random loot drop rolling gold roll");
                break;
            default:
                Debug.LogWarning("No loot prefab found for random loot drop, which likely is out of range");
                break;
        }
    }
}
