using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Knockback knockback;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {        
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (knockback.KnockbackActive) { return; }
        Move();

        if (moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Move()
    {   
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));        
    }

    public void SetTarget(Vector2 targetPos)
    {
        moveDirection = targetPos;
    }
}
