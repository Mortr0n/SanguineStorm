using UnityEngine;

public class CombatActor2D : MonoBehaviour
{
    [SerializeField] protected int factionID = 0;
    [SerializeField] protected float damage = 1;



    public virtual void InitializeDamage(float amount)
    {
        damage = amount;
    }
    public virtual void SetFactionID(int newID)
    {
        factionID = newID;
    }

    protected virtual void HitReceiver(CombatReceiver2D target)
    {
        target.TakeDamage(damage);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CombatReceiver2D>() != null && collision.isTrigger)
        {
            if (collision.GetComponent<CombatReceiver2D>().GetFactionID() != factionID)
                HitReceiver(collision.GetComponent<CombatReceiver2D>());
        }
    }
}

