using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class VS_PlayerController : MonoBehaviour
{
    VS_PlayerMovement movement;
    VS_PlayerCharacterSheet characterSheet;
    VS_PlayerAnimator animator;
    VS_PlayerCombat combat;

    [SerializeField] List<VS_BaseWeapon> equippedWeapons = new List<VS_BaseWeapon>();
    [SerializeField] List<VS_BaseWeapon> allWeapons = new List<VS_BaseWeapon>();

    bool alive = true;

    public static VS_PlayerController instance;

    private void Awake()
    {
        if (instance == null) instance = this;

        movement = GetComponent<VS_PlayerMovement>();
        characterSheet = GetComponent<VS_PlayerCharacterSheet>();
        animator = GetComponent<VS_PlayerAnimator>();
        combat = GetComponent<VS_PlayerCombat>();
        EquipWeaponById(WeaponIdentifier.MagicElectricBallWeapon); 
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
        foreach (VS_BaseWeapon weapon in equippedWeapons)
        {
            
            if (weapon != null) weapon.RunWeapon();
        }
    }

    public bool EquipWeaponById(WeaponIdentifier weaponId)
    {
        if (HasWeapon(weaponId)) return false; // Already equipped

        // Find the weapon in allWeapons list
        VS_BaseWeapon weapon = allWeapons.Find(w => w.WeaponId == weaponId);
        if (weapon == null) return false; // Weapon not found
        
        Debug.Log($"Equipping weapon with ID: {weaponId} and name: {weapon.name}");
        return EquipWeapon(weapon);
    }

    public bool EquipWeapon(VS_BaseWeapon weapon)
    {
        if (HasWeapon(weapon.WeaponId)) return false;

        equippedWeapons.Add(weapon);
        Debug.Log($"Equipped weapon: {weapon.name} with ID: {weapon.WeaponId}");
        return true;
    }

    public int RollRandomWeaponIndex()
    {
        if (allWeapons.Count == 0) return -1; // No weapons available
        return Random.Range(0, allWeapons.Count);
    }

    public VS_BaseWeapon GetRandomWeapon()
    {
        int randomIndex = RollRandomWeaponIndex();
        if (randomIndex == -1) return null; // No weapons available
        return GetWeaponFromAllWeapons(randomIndex);
    }

    public VS_BaseWeapon GetWeaponFromAllWeapons(int itemNumber)
    {
        return allWeapons[itemNumber];
    }

    public bool HasWeapon(WeaponIdentifier weaponId)
    {
        return equippedWeapons.Exists(weapon => weapon.WeaponId == weaponId);
    }

    public void TriggerDeath()
    {
        GetComponent<VS_PlayerAnimator>().TriggerDeathAnimation();
        alive = false;
        EventsManager.instance.onPlayerDied.Invoke();
    }
}
