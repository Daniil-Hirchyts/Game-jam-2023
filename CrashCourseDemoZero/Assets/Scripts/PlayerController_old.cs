using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_old : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    SpriteRenderer spriteRenderer;
    Animator animator;
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool succes = TryMove(movementInput);
            if (!succes)
            {
                succes = TryMove(new Vector2(movementInput.x, 0));
            }

            if (!succes)
            {
                succes = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetBool(("isMoving"), succes);
        }
        else
        {
            animator.SetBool(("isMoving"), false);
        }

        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(direction, movementFilter, castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * (moveSpeed * Time.fixedDeltaTime));
                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}