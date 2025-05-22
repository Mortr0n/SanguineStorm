using UnityEngine;
using TMPro;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] GameObject statPanel;
    [SerializeField] GameObject buttonPanel;
    [Space(10)]
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

    private void Start()
    {
        statPanel.SetActive(false);
        buttonPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        statPanel.SetActive(true);
        buttonPanel.SetActive(true);
        UpdateStatPanel();
        FindFirstObjectByType<RoundManager>().TogglePause();
    }
    public void ClosePanel() 
    {
        statPanel.SetActive(false);
        buttonPanel.SetActive(false);
        FindFirstObjectByType<RoundManager>().TogglePause();
        TooltipManager.instance.HideTooltip();
    }

    void SpentPoints()
    {
        EventsManager.instance.onPlayerChoseLevelUpgrade.Invoke();
        ClosePanel();
    }
    void UpdateStatPanel()
    {
        CharacterStats stats = VS_PlayerCharacterSheet.instance.Stats();

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

    public void SelectHP()
    {
        VS_PlayerCharacterSheet.instance.AddMaxHealth(10);
        SpentPoints();
    }
    public void SelectRecovery()
    {
        VS_PlayerCharacterSheet.instance.AddRecovery(.1f);
        SpentPoints();
    }
    public void SelectArmor()
    {
        VS_PlayerCharacterSheet.instance.AddArmor(1);
        SpentPoints();
    }
    public void SelectSpeed()
    {
        VS_PlayerCharacterSheet.instance.AddSpeed(.1f);
        SpentPoints();
    }
    public void SelectMight()
    {
        VS_PlayerCharacterSheet.instance.AddMight(.1f);
        SpentPoints();
    }
    public void SelectProjectSpeed()
    {
        VS_PlayerCharacterSheet.instance.AddProjectileSpeed(.1f);
        SpentPoints();
    }
    public void SelectDuration()
    {
        VS_PlayerCharacterSheet.instance.AddDuration(1);
        SpentPoints();
    }
    public void SelectArea()
    {
        VS_PlayerCharacterSheet.instance.AddArea(.1f);
        SpentPoints();
    }
    public void SelectCooldown()
    {
        VS_PlayerCharacterSheet.instance.AdjustCooldownMod(.1f);
        SpentPoints();
    }
    public void SelectMagnet()
    {
        VS_PlayerCharacterSheet.instance.AddMagnet(.3f);
        SpentPoints();
    }
}
