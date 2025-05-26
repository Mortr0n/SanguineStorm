using System.Collections;
using UnityEngine;

public class MagicElectricBallActor : WeaponActor
{
    private float speed = 2f;
    private Vector2 moveDirection;
    private float ballTimeToDestroy = 3f;

    [SerializeField] float maxScale = 3f;
    // Some people would see that they screwed up on the sizing of the sprites and colliders and choose to fix that
    // not me.  I chose to add a magic number and call myself out in the code.  Welcome to the big leagues!

    public new float baseSpeed = .5f;

    public override WeaponActorIdentifier WeaponActorIdentifier => WeaponActorIdentifier.MagicElectricBallActor;

    public void Initialize(WeaponStatModifiers weaponstatModifiers)
    {
        _weaponStatModifiers = weaponstatModifiers;
        //TODO: Test after I've got everything working
        if (_weaponStatModifiers == null)
        {
            Debug.LogWarning("MagicElectricBallActor: weaponStatModifiers is null. Please initialize it before calling Initialize.");
            return;
        }
        if (_weaponStatModifiers != null)
        {
            SetAttackArea();

            moveDirection = (target.transform.position - transform.position).normalized;
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

    



    private void Update()
    {
        if (moveDirection == Vector2.zero)
        {
            Debug.LogWarning("MagicElectricBallActor: moveDirection is zero — maybe target wasn’t set?");
        }
        transform.position += (Vector3)(moveDirection * baseSpeed * Time.deltaTime);
    }


    public void FinishAnimationAndDestroy()
    {
        StartCoroutine(TriggerEndAnimation());        
    }

    public IEnumerator TriggerEndAnimation()
    {
        Animator thisAnimator = GetComponent<Animator>();
        yield return new WaitForSeconds(ballTimeToDestroy);

        thisAnimator.SetBool("CastComplete", true);

        if (thisAnimator.GetBool("CastComplete")) Destroy(gameObject, .3f);
    }

    // Original way just in case I screw it up
    //public void Initialize()
    //{
    //    CircleCollider2D collider = GetComponent<CircleCollider2D>();
    //    collider.radius *= magicColliderMultiple;
    //    float areaStat = VS_PlayerCharacterSheet.instance.Stats().area;
    //    var area = GetAttackArea();
    //    if (areaStat < maxScale)
    //    {
    //        transform.localScale *= VS_PlayerCharacterSheet.instance.Stats().area;
    //        collider.radius *= VS_PlayerCharacterSheet.instance.Stats().area * magicColliderMultiple;
    //    }
    //    else
    //    {
    //        transform.localScale *= maxScale;
    //        collider.radius *= maxScale * magicColliderMultiple;
    //    }

    //    moveDirection = (target.transform.position - transform.position).normalized;
    //    FinishAnimationAndDestroy();
    //}
}
