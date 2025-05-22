using UnityEngine;

public class VS_PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseSpeed = 5.0f;

    public void RunMovement_FixedUpdate()
    {
        RunMovement();
    }

    void RunMovement()
    {

        Vector2 moveDirection = GetMoveDirection().normalized * baseSpeed;
        moveDirection *= GetComponent<VS_PlayerCharacterSheet>().Stats().speedMod;
        transform.Translate(moveDirection * Time.fixedDeltaTime);
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) moveDirection += Vector2.up;    // ( 0,  1)
        if (Input.GetKey(KeyCode.A)) moveDirection += Vector2.left;  // (-1,  0)
        if (Input.GetKey(KeyCode.S)) moveDirection += Vector2.down;  // ( 0, -1)
        if (Input.GetKey(KeyCode.D)) moveDirection += Vector2.right; // ( 1,  0)

        return moveDirection;
    }
}
