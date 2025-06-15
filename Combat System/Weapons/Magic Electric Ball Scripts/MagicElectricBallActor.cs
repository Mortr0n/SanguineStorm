using System.Collections;
using UnityEngine;

public class MagicElectricBallActor : WeaponActor
{
    //private float speed = 2f;
    
    
    //private float ballTimeToDestroy = 2f;

    [SerializeField] float maxScale = 3f;
    // Some people would see that they screwed up on the sizing of the sprites and colliders and choose to fix that
    // not me.  I chose to add a magic number and call myself out in the code.  Welcome to the big leagues!



    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.MagicElectricBallActor;

    public override void Initialize(WeaponStatModifiers weaponStatModifiers, float baseDamage)
    {
         
        base.Initialize(weaponStatModifiers, baseDamage);

        Debug.DrawRay(transform.position, moveDirection.normalized * 2f, Color.cyan);
        _weaponStatModifiers = weaponStatModifiers;
        //TODO: Test after I've got everything working
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("MagicElectricBallActor: weaponStatModifiers is null. Please initialize it before calling Initialize.");
            return;
        }
        if (_weaponStatModifiers != null)
        {
            SetAttackArea();
            //moveDirection = (target.transform.position - transform.position).normalized;
            FinishAnimationAndDestroy();
        }
        
    }

    public override void SetAttackArea()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        var area = GetAttackArea();
        transform.localScale *= area;
        collider.radius *= area * magicColliderMultiple;
    }

    protected override void Update()
    {
        if (moveDirection == Vector2.zero)
        {
            Debug.LogWarning("MagicElectricBallActor: moveDirection is zero — maybe target wasn’t set?");
        }
        Debug.Log($"MagicElectricBallActor: Moving in direction {moveDirection} with speed {GetProjectileSpeed()}");
        //transform.position += (Vector3)(moveDirection * (projectileSpeed) * Time.deltaTime);
        transform.position += (Vector3)(moveDirection * (GetProjectileSpeed()/2) * Time.deltaTime);
    }


    public void FinishAnimationAndDestroy()
    {
        StartCoroutine(TriggerEndAnimation());        
    }

    public IEnumerator TriggerEndAnimation()
    {
        Animator thisAnimator = GetComponent<Animator>();
        yield return new WaitForSeconds(GetProjectileDuration());

        thisAnimator.SetBool("CastComplete", true);

        if (thisAnimator.GetBool("CastComplete")) Destroy(gameObject, .3f);
    }


}
