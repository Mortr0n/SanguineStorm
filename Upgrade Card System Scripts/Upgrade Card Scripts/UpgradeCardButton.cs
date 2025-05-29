using System;
using UnityEngine;

public class UpgradeCardButton : MonoBehaviour
{
    [SerializeField] private UpgradeCard_SO upgradeCard;
    [SerializeField] private UpgradeCardDisplay upgradeCardDisplay;
    void Start()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnCardClicked);
    }

    private void OnCardClicked()
    {
        upgradeCard.Apply();
        Debug.Log($"Upgrade card {upgradeCard.name} applied.");
        UIManager.instance.levelUpPanel.ClosePanel();
        EventsManager.instance.onPlayerChoseLevelUpgrade?.Invoke();
    }

    public void SetCard(UpgradeCard_SO card)
    {
        upgradeCard = card;
        upgradeCardDisplay?.Initialize(card);
    }
}
