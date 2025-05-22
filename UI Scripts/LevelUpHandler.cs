using UnityEngine;

public class LevelUpHandler : MonoBehaviour
{
    void Start()
    {
        EventsManager.instance.onPlayerLeveledUp.AddListener(PHLevel);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(PHLevel);
    }
    

    void PHLevel()
    {
        UIManager.instance.TriggerLevelUpPanel();
    }
}
