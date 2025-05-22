using UnityEngine;
using TMPro;

public class PermanentUpgradeMenu : MonoBehaviour
{
    [SerializeField] GameObject statPanel;
    [SerializeField] GameObject buttonPanel;

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI recoveryText;
    [SerializeField] TextMeshProUGUI armorText;
    [SerializeField] TextMeshProUGUI speedText;
    [SerializeField] TextMeshProUGUI mightText;
    [SerializeField] TextMeshProUGUI projectSpeedText;
    [SerializeField] TextMeshProUGUI durationText;
    [SerializeField] TextMeshProUGUI areaText;
    [SerializeField] TextMeshProUGUI cooldownText;
    [SerializeField] TextMeshProUGUI magnetText;

    [Space(10)]
    [SerializeField] TextMeshProUGUI hpCostText;
    [SerializeField] TextMeshProUGUI recoveryCostText;
    [SerializeField] TextMeshProUGUI armorCostText;
    [SerializeField] TextMeshProUGUI speedCostText;
    [SerializeField] TextMeshProUGUI mightCostText;
    [SerializeField] TextMeshProUGUI projectileSpeedCostText;
    [SerializeField] TextMeshProUGUI durationCostText;
    [SerializeField] TextMeshProUGUI areaCostText;
    [SerializeField] TextMeshProUGUI cooldownCostText;
    [SerializeField] TextMeshProUGUI magnetCostText;

    int hpLevel = 1;
    int recoveryLevel = 1;
    int armorLevel = 1;
    int speedLevel = 1;
    int mightLevel = 1;
    int projectileSpeedLevel = 1;
    int durationLevel = 1;
    int areaLevel = 1;
    int cooldownLevel = 1;
    int magnetLevel = 1;

    public static PermanentUpgradeMenu instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        buttonPanel.SetActive(false);
        statPanel.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onGoldSpent.AddListener(UpdateStatPanel);
        EventsManager.instance.onGoldSpent.AddListener(UpdateCostTexts);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onGoldSpent.RemoveListener(UpdateStatPanel);
        EventsManager.instance.onGoldSpent.RemoveListener(UpdateCostTexts);
    }

    public void OpenPanel()
    {
        buttonPanel.SetActive(true);
        statPanel.SetActive(true);
        UpdateStatPanel();
        UpdateCostTexts();
    }
    public void ClosePanel()
    {
        buttonPanel.SetActive(false);
        statPanel.SetActive(false);
    }
    void UpdateStatPanel()
    {
        CharacterStats stats = UpgradeManager.instance.GetPermanentStats();

        hpText.text = "HP: " + stats.maxHealth.ToString("F2");
        recoveryText.text = "Recovery: " + stats.recovery.ToString("F2");
        armorText.text = "Armor: " + stats.armor.ToString("F2");
        speedText.text = "Speed: " + stats.speedMod.ToString("F2");
        mightText.text = "Might: " + stats.might.ToString("F2");
        projectSpeedText.text = "Projectile Speed: " + stats.projectileSpeed.ToString("F2");
        durationText.text = "Duration: " + stats.duration.ToString("F2");
        areaText.text = "Area: " + stats.area.ToString("F2");
        cooldownText.text = "Cooldown: " + stats.cooldown.ToString("F2");
        magnetText.text = "Magnet: " + stats.magnet.ToString("F2");
    }
    void UpdateCostTexts()
    {
        hpCostText.text = GetNextLevelCost(hpLevel).ToString();
        recoveryCostText.text = GetNextLevelCost(recoveryLevel).ToString();
        armorCostText.text = GetNextLevelCost(armorLevel).ToString();
        speedCostText.text = GetNextLevelCost(speedLevel).ToString();
        mightCostText.text = GetNextLevelCost(mightLevel).ToString();
        projectileSpeedCostText.text = GetNextLevelCost(projectileSpeedLevel).ToString();
        durationCostText.text = GetNextLevelCost(durationLevel).ToString();
        areaCostText.text = GetNextLevelCost(areaLevel).ToString();
        cooldownCostText.text = GetNextLevelCost(cooldownLevel).ToString();
        magnetCostText.text = GetNextLevelCost(magnetLevel).ToString();
    }

    void SpentPoints()
    {
        EventsManager.instance.onGoldSpent.Invoke();
    }
    public void SelectHP()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(hpLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(hpLevel));
            hpLevel++;
            UpgradeManager.instance.AddMaxHealth(10);
            SpentPoints();
        }
    }
    public void SelectRecovery()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(recoveryLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(recoveryLevel));
            recoveryLevel++;
            UpgradeManager.instance.AddRecovery(.1f);
            SpentPoints();
        }
    }
    public void SelectArmor()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(armorLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(armorLevel));
            armorLevel++;
            UpgradeManager.instance.AddArmor(1);
            SpentPoints();
        }
    }
    public void SelectSpeed()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(speedLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(speedLevel));
            speedLevel++;
            UpgradeManager.instance.AddSpeed(.1f);
            SpentPoints();
        }
    }
    public void SelectMight()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(mightLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(mightLevel));
            mightLevel++;
            UpgradeManager.instance.AddMight(.1f);
            SpentPoints();
        }
    }
    public void SelectProjectSpeed()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(projectileSpeedLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(projectileSpeedLevel));
            projectileSpeedLevel++;
            UpgradeManager.instance.AddProjectileSpeed(.1f);
            SpentPoints();
        }
    }
    public void SelectDuration()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(durationLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(durationLevel));
            durationLevel++;
            UpgradeManager.instance.AddDuration(1);
            SpentPoints();
        }
    }
    public void SelectArea()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(areaLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(areaLevel));
            areaLevel++;
            UpgradeManager.instance.AddArea(.1f);
            SpentPoints();
        }
    }
    public void SelectCooldown()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(cooldownLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(cooldownLevel));
            cooldownLevel++;
            UpgradeManager.instance.AdjustCooldownMod(.1f);
            SpentPoints();
        }
    }
    public void SelectMagnet()
    {
        if (UpgradeManager.instance.GetGold() >= GetNextLevelCost(magnetLevel))
        {
            UpgradeManager.instance.SpendGold(GetNextLevelCost(magnetLevel));
            magnetLevel++;
            UpgradeManager.instance.AddMagnet(.3f);
            SpentPoints();
        }
    }

    int GetNextLevelCost(int currentLevel)
    {
        return currentLevel * 100;
    }
}
