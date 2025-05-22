using UnityEngine;

public class VS_BasicEnemyCombat : CombatReceiver2D
{
    public override void Die()
    {
        base.Die();
        GetComponent<VS_BasicEnemyController>().TriggerDeath();
    }
}
