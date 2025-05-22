using UnityEngine;

public class MargaritaActor : WeaponActor
{
    Vector3 moveVector = Vector3.zero;
    [SerializeField] float moveSpeed = 7;
    [SerializeField] GameObject explosionObject;

    private void Start()
    {
        Destroy(gameObject, 7);
    }
    void FixedUpdate()
    {
        // Moves by the base projectileSpeed modified by the projectile speed upgrade stat
        transform.Translate(moveVector * Time.fixedDeltaTime * moveSpeed * VS_PlayerCharacterSheet.instance.Stats().projectileSpeed);
    }

    public override void SetTarget(GameObject newTarget)
    {
        float ranX = Random.Range(-5f, 5f);
        float ranY = Random.Range(-5f, 5f);

        moveVector = newTarget.transform.position - transform.position + new Vector3(ranX, ranY, 0);
    }
    protected override void HitReceiver(CombatReceiver2D target)
    {
        Instantiate(explosionObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
