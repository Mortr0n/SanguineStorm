using UnityEngine;
using System;
using Unity.VisualScripting;

public class VS_PlayerCharacterSheet : MonoBehaviour
{
    CharacterStats stats;

    int currentLevel = 1;
    int xpPerLevel = 100;
    int currentExperience = 0;

    public static VS_PlayerCharacterSheet instance;

    private void Awake()
    {
        if(instance == null) instance = this;

        stats = new CharacterStats();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onExperienceGained.AddListener(AddXP);
        if (UpgradeManager.instance != null) stats = UpgradeManager.instance.GetPermanentStats();
    }

    public CharacterStats Stats()
    {
        return stats;
    }

    #region Levels
    public void AddXP(int amount)
    {
        currentExperience += amount;
        Debug.Log("XP Gained: " + amount);
        if (currentExperience >= XPToNextLevel()) LevelUp();
    }

    int XPToNextLevel()
    {
        return xpPerLevel * currentLevel;
    }

    void LevelUp()
    {
        Debug.Log($"leveling up to level {currentLevel + 1} {XPToNextLevel()}");
        currentExperience -= XPToNextLevel();
        currentLevel++;
        // Trigger Level Up Event
        EventsManager.instance.onPlayerLeveledUp.Invoke();
    }
    #endregion

    #region Modify Stat Functions
    public void AddMaxHealth(float amount)
    {
        stats.maxHealth += amount;
    }
    public void AddRecovery(float amount)
    {
        stats.recovery += amount;
    }
    public void AddArmor(float amount)
    {
        stats.armor += amount;
    }
    public void AddSpeed(float amount)
    {
        stats.speedMod += amount;
    }

    public void AddMight(float amount)
    {
        stats.might += amount;
    }
    public void AddProjectileSpeed(float amount)
    {
        stats.projectileSpeed += amount;
    }
    public void AddDuration(float amount)
    {
        stats.duration += amount;
    }
    public void AddArea(float amount)
    {
        stats.area += amount;
    }

    public void AdjustCooldownMod(float amount)
    {
        stats.cooldown *= amount;
    }
    public void AddAmount(int amount)
    {
        stats.amount += amount;
    }
    public void AddMagnet(float amount)
    {
        stats.magnet += amount;
    }
    #endregion
}

[Serializable]
public class CharacterStats
{
    public float maxHealth = 100;
    public float recovery = .5f;
    public float armor = 0;
    public float speedMod = 1;

    public float might = 1;
    public float projectileSpeed = 2f;
    public float duration = 1;
    public float area = 1;

    public float cooldown = 1;
    public int amount = 1;
    public float magnet = 1;

    public float GetStat(CharacterStatType type)
    {
        return type switch
        {
            CharacterStatType.MaxHealth => maxHealth,
            CharacterStatType.Recovery => recovery,
            CharacterStatType.Armor => armor,
            CharacterStatType.Speed => speedMod,
            CharacterStatType.Might => might,
            CharacterStatType.ProjectileSpeed => projectileSpeed,
            CharacterStatType.Duration => duration,
            CharacterStatType.Area => area,
            CharacterStatType.Cooldown => cooldown,
            CharacterStatType.Magnet => magnet,
            _ => 0f,
        };
    }

    public void ModifyStat(CharacterStatType type, float amount)
    {
        switch (type)
        {
            case CharacterStatType.MaxHealth: maxHealth += amount; break;
            case CharacterStatType.Recovery: recovery += amount; break;
            case CharacterStatType.Armor: armor += amount; break;
            case CharacterStatType.Speed: speedMod += amount; break;
            case CharacterStatType.Might: might += amount; break;
            case CharacterStatType.ProjectileSpeed: projectileSpeed += amount; break;
            case CharacterStatType.Duration: duration += amount; break;
            case CharacterStatType.Area: area += amount; break;
            case CharacterStatType.Cooldown: cooldown *= amount; break; 
            case CharacterStatType.Magnet: magnet += amount; break;
        }
    }
}
