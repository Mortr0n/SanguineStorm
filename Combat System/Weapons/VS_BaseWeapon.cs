using UnityEngine;
using System.Collections.Generic;

public class VS_BaseWeapon : MonoBehaviour
{
    private void Update()
    {
        transform.localPosition = Vector3.zero;
    }

    public virtual void RunWeapon()
    {

    }

    public GameObject GetClosestTarget(List<GameObject> enemiesInRange)
    {
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToEnemy = Vector3.Distance(enemy.transform.position, currentPosition);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                return enemy;
                
            }
        }
        return null;
    }
}


