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
        if (other.gameObject.CompareTag("Ground")) isJumping = false;
        Animator anim = GetComponent<Animator>();
        anim.SetBool("isJumping", false);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) isJumping = true;
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
