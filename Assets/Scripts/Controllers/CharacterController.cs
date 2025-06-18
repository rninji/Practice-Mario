using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private PlayerState state = PlayerState.Idle;
    
    private SpriteRenderer spriteRenderer;
    
    Rigidbody2D characterRb;
    
    private float h;
    
    [SerializeField] 
    float speed = 3.0f;

    [SerializeField] 
    float jumpPower = 3.0f;

    private bool isJumping = false;

    private int coinScore = 0;

    private int lifePoints = 3;
    
    public Vector2 startPosition;

    public enum PlayerState
    {
        Idle,
        Moving,
        Jumping,
        Dead,
    }

    void UpdateIdle()
    {
        
    }

    void UpdateMoving()
    {
        h = Input.GetAxis("Horizontal");
        if (Math.Abs(h) < 0.001f)
        {
            h = 0f;
            state = PlayerState.Idle;
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("moving", 0);
        }
            
    }

    void UpdateJumping()
    {
        
    }

    void UpdateDead()
    {
        lifePoints--;
        Debug.Log($"죽었습니다. 남은 생명 : {lifePoints}개");
        
        if (lifePoints <= 0)
        {
            Debug.Log("Game Over");
            lifePoints = 3;
        }
        transform.position = startPosition;
        h = 0f;
        state = PlayerState.Moving;
    }
    
    void Update()
    {
        switch (state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Jumping:
                UpdateJumping();
                break;
            case PlayerState.Dead:
                UpdateDead();
                break;
        }
    }
    
    void Start()
    {
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        
        characterRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // 바닥 충돌 (점프 체크)
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            Animator anim = GetComponent<Animator>();
            anim.SetBool("isJumping", false);
        }
        // 장애물 충돌
        if (other.gameObject.CompareTag("Spike"))
        {
            state = PlayerState.Dead;
        }
        // 몬스터 충돌
        if (other.gameObject.CompareTag("Monster"))
        {
            // 위에서 밟은 경우 공격
            if (other.gameObject.transform.position.y + other.gameObject.transform.localScale.y * 0.7f < transform.position.y)
            {
                Animator anim = GetComponent<Animator>();
                Destroy(other.gameObject);
                anim.SetBool("isJumping", true);
                state = PlayerState.Jumping;
                characterRb.AddForceY(jumpPower, ForceMode2D.Impulse);
            }
            else
            {
                // 아래나 옆에서 충돌한 경우 피격
                state = PlayerState.Dead;
            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) isJumping = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 코인 획득
        if (other.gameObject.CompareTag("Coin"))
        {
            coinScore++;
            Debug.Log($"코인 획득 : {coinScore}개");
            Destroy(other.gameObject);
        }
        
        // 최종 지점 도착
        if (other.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Clear!!!");
        }
    }

    void OnKeyboard()
    {
        Animator anim = GetComponent<Animator>();
        // 이동
        h = Input.GetAxis("Horizontal");
        if (Math.Abs(h) > 0)
        {
            anim.SetFloat("moving", Math.Abs(h));
            state = PlayerState.Moving;
        }
        // 점프
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            anim.SetBool("isJumping", true);
            state = PlayerState.Jumping;
            characterRb.AddForceY(jumpPower, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (h > 0.001)
        {
            spriteRenderer.flipX = false;
        }
        else if (h < -0.001)
        {
            spriteRenderer.flipX = true;
        }
        characterRb.linearVelocityX = h * speed;
    }
}
