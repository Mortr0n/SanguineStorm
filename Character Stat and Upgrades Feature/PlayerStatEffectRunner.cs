using UnityEngine;

public class PlayerStatEffectRunner : MonoBehaviour
{
    [SerializeField] private StatEffect_SO[] effects;

    private void Start()
    {
        var stats = VS_PlayerCharacterSheet.instance.GetComponent<CharacterStats>();
        foreach (var effect in effects)
        {
            effect.ApplyToPlayer(stats);
        }
    }
}
