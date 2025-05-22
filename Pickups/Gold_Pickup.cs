using UnityEngine;

public class Gold_Pickup : VS_BasePickup
{
    [SerializeField] int goldValue = 35;
    protected override void Collected()
    {
        AudioManager.instance.PlayPickupGoldSFX();
        EventsManager.instance.onGoldPickedUp.Invoke(goldValue);
        base.Collected();
    }
}
