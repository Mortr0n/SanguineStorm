using UnityEngine;

public class PH_LevelUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventsManager.instance.onPlayerLeveledUp.AddListener(PHLevel);
    }
    private void OnDestroy()
    {
        EventsManager.instance.onPlayerLeveledUp.RemoveListener(PHLevel);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) PHLevel();
    }

    void PHLevel()
    {
        VS_PlayerCharacterSheet.instance.AddMaxHealth(1);
        VS_PlayerCharacterSheet.instance.AddSpeed(.1f);
        VS_PlayerCharacterSheet.instance.AddProjectileSpeed(.1f);
        VS_PlayerCharacterSheet.instance.AddMight(.1f);
        VS_PlayerCharacterSheet.instance.AddArea(.1f);
        VS_PlayerCharacterSheet.instance.AdjustCooldownMod(.9f);
    }
}
