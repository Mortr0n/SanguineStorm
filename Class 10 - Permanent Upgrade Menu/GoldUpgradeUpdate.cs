using UnityEngine;
using TMPro;

public class GoldUpgradeUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldCountText;
    void Start()
    {
        EventsManager.instance.onGoldSpent.AddListener(UpdateGoldCount);
    }

    private void OnDestroy()
    {
        EventsManager.instance.onGoldSpent.RemoveListener(UpdateGoldCount);
    }

    void OnEnable()
    {
        UpdateGoldCount();
    }

    void UpdateGoldCount()
    {
        goldCountText.text = UpgradeManager.instance.GetGold().ToString();
    }
}
