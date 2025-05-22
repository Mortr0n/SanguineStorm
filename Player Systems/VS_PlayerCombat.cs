using UnityEngine;

public class VS_PlayerCombat : CombatReceiver2D
{
    private void Update()
    {
        if(alive) RegenerateHP();
    }
    void RegenerateHP()
    {
        if(currentHP < GetMaxHP())
        {
            currentHP += GetComponent<VS_PlayerCharacterSheet>().Stats().recovery * Time.deltaTime;

            if(currentHP > GetMaxHP()) currentHP = GetMaxHP();
        }
    }
    public override void Die()
    {
        base.Die();
        GetComponent<VS_PlayerController>().TriggerDeath();
    }

    public override float GetMaxHP()
    {
        return VS_PlayerCharacterSheet.instance.Stats().maxHealth;
    }
}
