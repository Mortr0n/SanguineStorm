using UnityEngine;

public class VS_BasePickup : MonoBehaviour
{
    Transform targetToFlyTo = null;

    private void FixedUpdate()
    {
        if(targetToFlyTo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetToFlyTo.position, 10 * Time.fixedDeltaTime);

            if (Vector3.Distance(targetToFlyTo.position, transform.position) < .5f) Collected();
        }
    }

    protected virtual void Collected()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            targetToFlyTo = collision.transform;
        }
    }

}
