using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 150f;

    [SerializeField] 
    float flipTime = 2f;

    bool isRight = false;
    
    SpriteRenderer spriteRenderer;
    Rigidbody2D monsterRb;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterRb = GetComponent<Rigidbody2D>();
        
        InvokeRepeating("Flip", flipTime, flipTime);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isMoving", true);
        monsterRb.linearVelocityX = isRight ? moveSpeed * Time.deltaTime : -moveSpeed * Time.deltaTime;
    }

    void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        isRight = spriteRenderer.flipX;
    }
}
