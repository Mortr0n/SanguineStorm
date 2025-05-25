using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class FireBombWeapon : VS_BaseWeapon
{
    public override WeaponIdentifier WeaponId => WeaponIdentifier.FireBombWeapon;

    [SerializeField] GameObject fireBombPrefab;
    [SerializeField] GameObject enemyTarget;
    [SerializeField] List<GameObject> enemiesInRange = new();

    protected float spawnTime = 1f;
    protected float spawnTimer = 0f;
    protected float damage = 1f;

    public override void RunWeapon()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnTime)
        {
            enemyTarget = GetClosestTarget(enemiesInRange);
            if (enemyTarget != null)
            {
                Vector3 targetPosition = enemyTarget.transform.position;
                GameObject fireBomb = Instantiate(fireBombPrefab, targetPosition, Quaternion.identity);

                fireBomb.GetComponent<WeaponActor>().SetTarget(enemyTarget); 
            }            
            spawnTimer -= spawnTime;
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

    //private void GetClosestTarget()
    //{
    //    float closestDistance = Mathf.Infinity;
    //    Vector3 currentPosition = transform.position;
    //    foreach (GameObject enemy in enemiesInRange)
    //    {
    //        float distanceToEnemy = Vector3.Distance(enemy.transform.position, currentPosition);
    //        if (distanceToEnemy < closestDistance)
    //        {
    //            closestDistance = distanceToEnemy;
    //            enemyTarget = enemy;
    //        }
    //    }
    //}

}
