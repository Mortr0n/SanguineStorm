using UnityEngine;

public class ActorStatEffectRunner : MonoBehaviour
{
    [SerializeField] private WeaponActor actor;
    [SerializeField] private StatEffect_SO[] effects;

    public void ApplyEffects()
    {
        if (actor == null)
        {
            actor = GetComponent<WeaponActor>();
        }
        var stats = VS_PlayerCharacterSheet.instance.GetComponent<CharacterStats>();
        foreach (var effect in effects)
        {
            effect.ApplyToActor(actor, stats);
        }
    }
}
