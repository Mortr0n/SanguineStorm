using UnityEngine;
using System.Collections;

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
        //StartCoroutine(DespawnAfterDelay(GetProjectileDuration()));
        //Destroy(gameObject, 7);
    }

    public override void Initialize(WeaponStatModifiers weaponStatModifiers, float baseDamage)
    {
        baseDamage = .02f; // Set base damage to a very low value for TacoActor
        base.Initialize(weaponStatModifiers, baseDamage);
        if (tacoGraphic != null)
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
                Debug.LogWarning("TacoActor: useFixedDirection is true but fixedDirection is not set. Using moveVector instead.");
                moveDirection = Vector2.right; // Default direction if fixedDirection is not set
            }
        }
        else
        {
            moveDirection = useFixedDirection ? fixedDirection.Value : moveVector.normalized;
        }
        StartCoroutine(DespawnAfterDelay(GetProjectileDuration()));
    }

    private void DisableTaco()
    {
        ObjectPooler.Instance.ReturnToPool("Taco", gameObject);
    }
    protected override void Update()
    {
        if(tacoGraphic != null) tacoGraphic.transform.Rotate(0, 0, tacoRotationSpeed * Time.deltaTime);
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
        Debug.Log($"TacoActor: Hit receiver {target.name} with taco {name} for {damage} damage.");
        // Does damage modified by the might stat
        target.TakeDamage(damage);
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        Debug.Log($"TacoActor: Despawning after {delay} seconds. for taco {name}");
        yield return new WaitForSeconds(delay);
        Debug.Log($"TacoActor: Despawned. taco {name}");
        gameObject.SetActive(false);
        DisableTaco();
    }

}
