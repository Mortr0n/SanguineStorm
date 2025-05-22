using System.Collections;
using TMPro;
using UnityEngine;

public class GoldUI : TriggeredDisplayUIElement
{
    [SerializeField] TextMeshProUGUI goldCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        EventsManager.instance.onGoldPickedUp.AddListener(UpdateText);
    }

    void UpdateText(int gold)
    {
        TriggerComeOnScreen();
        goldCount.text = UpgradeManager.instance.GetGold().ToString();
    }
}

