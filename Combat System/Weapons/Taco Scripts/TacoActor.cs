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
        StartCoroutine(DespawnAfterDelay(GetProjectileDuration()));

        //Destroy(gameObject, 7);
        
    }

    public override void Initialize(WeaponStatModifiers weaponStatModifiers)
    {
        base.Initialize(weaponStatModifiers);
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
           
    }

    private void DisableTaco()
    {
        ObjectPooler.Instance.ReturnToPool("Taco", gameObject);
    }
    private void Update()
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
        // Does damage modified by the might stat
        target.TakeDamage(damage * VS_PlayerCharacterSheet.instance.Stats().might);
    }

    IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        DisableTaco();
    }

}
