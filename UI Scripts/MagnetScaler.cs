using UnityEngine;

public class MagnetScaler : MonoBehaviour
{
    float startingRadius = 5;

    void Start()
    {
        startingRadius = GetComponent<CircleCollider2D>().radius;
        EventsManager.instance.onPlayerChoseLevelUpgrade.AddListener(UpdateSize);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onPlayerChoseLevelUpgrade.RemoveListener(UpdateSize);
    }
    
    void UpdateSize()
    {
        transform.localScale = Vector3.one * startingRadius * VS_PlayerCharacterSheet.instance.Stats().magnet;
    }
}
