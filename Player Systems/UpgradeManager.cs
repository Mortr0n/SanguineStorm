using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    CharacterStats permanentStats = new CharacterStats();
    int goldCount = 0;

    public static UpgradeManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    #region Gold
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onGoldPickedUp.AddListener(AddGold);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onGoldPickedUp.RemoveListener(AddGold);
    }
    void AddGold(int amount)
    {
        goldCount += amount;
    }
    public int GetGold()
    {
        return goldCount;
    }
    public void SpendGold(int amount)
    {
        goldCount -= amount;
        EventsManager.instance.onGoldSpent.Invoke();
    }
    #endregion

    public CharacterStats GetPermanentStats()
    {
        return permanentStats;
    }

    #region Modify Stat Functions
    public void AddMaxHealth(float amount)
    {
        permanentStats.maxHealth += amount;
    }
    public void AddRecovery(float amount)
    {
        permanentStats.recovery += amount;
    }
    public void AddArmor(float amount)
    {
        permanentStats.armor += amount;
    }
    public void AddSpeed(float amount)
    {
        permanentStats.speedMod += amount;
    }

    public void AddMight(float amount)
    {
        permanentStats.might += amount;
    }
    public void AddProjectileSpeed(float amount)
    {
        permanentStats.projectileSpeed += amount;
    }
    public void AddDuration(float amount)
    {
        permanentStats.duration += amount;
    }
    public void AddArea(float amount)
    {
        permanentStats.area += amount;
    }

    public void AdjustCooldownMod(float amount)
    {
        permanentStats.cooldown *= amount;
    }
    public void AddAmount(int amount)
    {
        permanentStats.amount += amount;
    }
    public void AddMagnet(float amount)
    {
        permanentStats.magnet += amount;
    }
    #endregion
}
