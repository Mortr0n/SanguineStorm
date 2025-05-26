using UnityEngine;

public class TacoActor : WeaponActor
{
    Vector3 moveVector = Vector3.zero;
    [SerializeField] float moveSpeed = 7;
    [SerializeField] GameObject tacoGraphic;
    [SerializeField] float tacoRotationSpeed = 180;

    //CharacterStats characterStats;

    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.TacoActor;

    void Start()
    {
        Destroy(gameObject, 7);
        // Scale by area so it gets bigger as we upgrade area
        transform.localScale = Vector3.one * VS_PlayerCharacterSheet.instance.Stats().area;
    }

    private void Update()
    {
        if(tacoGraphic != null) tacoGraphic.transform.Rotate(0, 0, tacoRotationSpeed * Time.deltaTime);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Moves by the base projectileSpeed modified by the projectile speed upgrade stat
        transform.Translate(moveVector * Time.fixedDeltaTime * moveSpeed * VS_PlayerCharacterSheet.instance.Stats().projectileSpeed);
    }
    public override void SetTarget(GameObject newTarget)
    {
        moveVector = newTarget.transform.position - transform.position;
    }
    protected override void HitReceiver(CombatReceiver2D target)
    {
        // Does damage modified by the might stat
        target.TakeDamage(damage * VS_PlayerCharacterSheet.instance.Stats().might);
    }
}
