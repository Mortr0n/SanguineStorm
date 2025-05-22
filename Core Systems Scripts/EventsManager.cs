using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public UnityEvent<int> onExperienceGained;
    public UnityEvent onPlayerLeveledUp;
    public UnityEvent onPlayerChoseLevelUpgrade;

    public UnityEvent onHealthChanged;
    public UnityEvent onPlayerDied;

    public UnityEvent onStatsChanged;

    public UnityEvent<int> onGoldPickedUp;
    public UnityEvent onGoldSpent;

    public static EventsManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}

