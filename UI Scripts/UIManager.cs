using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public LevelUpPanel levelUpPanel;

    public UnityEvent onWindowResizedEvent;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RunWindowResize()
    {
        onWindowResizedEvent.Invoke();
    }

    public void TriggerLevelUpPanel()
    {
        levelUpPanel.OpenPanel();
    }
}
