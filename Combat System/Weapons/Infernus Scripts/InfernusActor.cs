using UnityEngine;
using System.Collections;

public class InfernusActor : WeaponActor
{
    Vector3 moveVector = Vector3.zero;
    [SerializeField] float moveSpeed = 7;
    [SerializeField] GameObject infernusGraphic;
    [SerializeField] float infernusRotationSpeed = 180;

    //CharacterStats characterStats;

    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.InfernusActor;

    void Start()
    {
        //StartCoroutine(DespawnAfterDelay(GetProjectileDuration()));
        //Destroy(gameObject, 7);
    }

    public override void Initialize(WeaponStatModifiers weaponStatModifiers, float baseDamage)
    {
        baseDamage = .02f; // Set base damage to a very low value for InfernusActor
        base.Initialize(weaponStatModifiers, baseDamage);
        if (infernusGraphic != null)
        {
            // Scale by area so it gets bigger as we upgrade area
            transform.localScale = Vector3.one * GetAttackArea();
        }
        if (useFixedDirection)
        {
            if (fixedDirection.HasValue)
            {
                moveDirection = fixedDirection.Value;
            }
            else
            {
                Debug.LogWarning("InfernusActor: useFixedDirection is true but fixedDirection is not set. Using moveVector instead.");
                moveDirection = Vector2.right; // Default direction if fixedDirection is not set
            }
        }
        else
        {
            moveDirection = useFixedDirection ? fixedDirection.Value : moveVector.normalized;
        }
        StartCoroutine(DespawnAfterDelay(GetProjectileDuration()));
    }

    private void DisableInfernus()
    {
        ObjectPooler.Instance.ReturnToPool("Infernus", gameObject);
    }
    protected override void Update()
    {
        if (infernusGraphic != null) infernusGraphic.transform.Rotate(0, 0, infernusRotationSpeed * Time.deltaTime);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // Moves by the base projectileSpeed modified by the projectile speed upgrade stat
        transform.Translate(moveDirection * Time.fixedDeltaTime * moveSpeed * GetProjectileSpeed());
    }
    public override void SetTarget(GameObject newTarget)
    {
        moveVector = newTarget.transform.position - transform.position;
    }
    protected override void HitReceiver(CombatReceiver2D target)
    {
        Debug.Log($"InfernusActor: Hit receiver {target.name} with infernus {name} for {damage} damage.");
        // Does damage modified by the might stat
        target.TakeDamage(damage);
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        Debug.Log($"InfernusActor: Despawning after {delay} seconds. for infernus {name}");
        yield return new WaitForSeconds(delay);
        Debug.Log($"InfernusActor: Despawned. infernus {name}");
        gameObject.SetActive(false);
        DisableInfernus();
    }

}
