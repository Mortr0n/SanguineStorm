using UnityEngine;

public class GarlicCombatActor : CombatActor2D
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayWandHitSFX();
        Destroy(gameObject, 1f);
    }
}
