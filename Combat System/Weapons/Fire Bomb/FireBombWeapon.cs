using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;

public class FireBombWeapon : VS_BaseWeapon
{
    public override WeaponIdentifier WeaponId => WeaponIdentifier.FireBombWeapon;

    [SerializeField] GameObject fireBombPrefab;
    [SerializeField] GameObject enemyTarget;
    [SerializeField] GameObject[] enemyTargets;
    HashSet<GameObject> enemiesInRangeSet = new HashSet<GameObject>();
    [SerializeField] List<GameObject> enemiesInRange = new();


    protected float spawnTime = 1f;
    protected float spawnTimer = 0f;
    protected float damage = 1f;

    public override void RunWeapon()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > GetProjectileCooldown())
        {
            AttackEnemies(enemiesInRange);
            spawnTimer -= spawnTime;
        }
    }

    public void AttackEnemies(List<GameObject> enemiesInRange)
    {
        List<GameObject> sortedEnemies = enemiesInRange.OrderBy(e => Vector3.Distance(transform.position, e.transform.position)).Take(GetProjectileAmount()).ToList();
        foreach (var target in sortedEnemies)
        {
            GameObject fireBomb = Instantiate(fireBombPrefab, target.transform.position, Quaternion.identity);
            WeaponActor firebombActor = fireBomb.GetComponent<WeaponActor>();
            firebombActor.Initialize(weaponStatModifiers);
            firebombActor.SetTarget(target);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CombatReceiver2D>() == null) return;

        if (other.gameObject.GetComponent<CombatReceiver2D>().GetFactionID() == 1)
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CombatReceiver2D receiver = other.gameObject.GetComponent<CombatReceiver2D>();
        if (receiver != null && receiver.GetFactionID() == 1)
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
