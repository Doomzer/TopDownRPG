using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D raycastHit;

    public float xSpeed = 1.0f;
    public float ySpeed = 0.75f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector2(input.x * xSpeed, input.y * ySpeed);

        if (moveDelta.x > 0)
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);

        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Block", "Unit"));
        if (raycastHit.collider == null)
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);

        raycastHit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Block", "Unit"));
        if (raycastHit.collider == null)
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
    }
}
