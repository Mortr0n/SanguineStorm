using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class VS_PlayerController : MonoBehaviour
{
    VS_PlayerMovement movement;
    VS_PlayerCharacterSheet characterSheet;
    VS_PlayerAnimator animator;
    VS_PlayerCombat combat;

    [SerializeField] List<VS_BaseWeapon> weapons = new List<VS_BaseWeapon>();

    bool alive = true;

    public static VS_PlayerController instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        movement = GetComponent<VS_PlayerMovement>();
        characterSheet = GetComponent<VS_PlayerCharacterSheet>();
        animator = GetComponent<VS_PlayerAnimator>();
        combat = GetComponent<VS_PlayerCombat>();
    }
    // Example inside VS_PlayerController
    public Vector2 GetMovementDirection()
    {
        Vector2 movementDirection = GetComponent<VS_PlayerMovement>().GetMoveDirection();
        return movementDirection.normalized; // assuming you already track this vector
    }


    // Update is called once per frame
    void Update()
    {
        if ((!alive)) return;
        RunWeapons();
    }

    private void FixedUpdate()
    {
        if ((!alive)) return;
        movement.RunMovement_FixedUpdate();
    }

    void RunWeapons()
    {
        foreach (VS_BaseWeapon weapon in weapons)
        {
            
            if (weapon != null) weapon.RunWeapon();
        }
    }

    public void TriggerDeath()
    {
        GetComponent<VS_PlayerAnimator>().TriggerDeathAnimation();
        alive = false;
        EventsManager.instance.onPlayerDied.Invoke();
    }
}
