using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image backgroundImage; //TODO: is this gonna work?
    [SerializeField] private Image iconImage;
    [SerializeField] private Button selectButton;

    private UpgradeCard_SO upgradeData;

    public void Initialize(UpgradeCard_SO data)
    {
        upgradeData = data;

        titleText.text = data.upgradeName;
        descriptionText.text = data.description;
        iconImage.sprite = data.icon;
        backgroundImage.color = data.rarity.GetRarityColor(); 

        // You could also tint the background or border based on rarity
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => upgradeData.Apply());
    }
}
