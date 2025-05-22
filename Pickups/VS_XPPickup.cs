using UnityEngine;

public class VS_XPPickup : VS_BasePickup
{
    [SerializeField] int xpValue = 35;

    protected override void Collected()
    {
        AudioManager.instance.PlayPickupXPSFX();
        EventsManager.instance.onExperienceGained.Invoke(xpValue);
        base.Collected();
    }
}
