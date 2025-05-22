using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator thisAnimator;
    [SerializeField] protected SpriteRenderer thisRenderer;

    protected Vector2 oldPos;
    protected Vector2 newPos;

    // These animation checks happen on FixedUpdates as movement only happens on FixedUpdate Physics frames
    public virtual void FixedUpdate()
    {
        newPos = transform.position;

        CheckAndSetRunning();
        FlipCheck();

        oldPos = transform.position;
    }

    protected virtual void CheckAndSetRunning()
    {
        // If we are moving less than .1 units per second, set the animation to an idle phase
        if (Vector2.Distance(newPos, oldPos) < .1f * Time.fixedDeltaTime) TriggerIdleAnimation();
        else TriggerMovingAnimation();
    }

    protected virtual void FlipCheck()
    {

        if (newPos.x - oldPos.x > 0) thisRenderer.flipX = false;
        else if ( newPos.x - oldPos.x < 0) thisRenderer.flipX = true;
    }

    public virtual void TriggerDeathAnimation()
    {
        thisAnimator.SetTrigger("Die");
    }
    public virtual void TriggerMovingAnimation()
    {
        thisAnimator.SetBool("Running", true);
    }
    public virtual void TriggerIdleAnimation()
    {
        thisAnimator.SetBool("Running", false);
    }
}
