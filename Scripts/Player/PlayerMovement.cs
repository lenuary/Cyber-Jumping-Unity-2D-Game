using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float TARGET_SCALE = 0.7f; 

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps = 1;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallSlideSpeed = 2f; 
    [SerializeField] private float wallJumpX = 10f; 
    [SerializeField] private float wallJumpY = 15f; 

    [Header("Wykrywanie Ziemi i Ścian")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float defaultGravityScale = 7f;

    [Header("Ustawienia Ataku")]
    [SerializeField] private float attackCooldown = 1f;
    private float cooldownTimer = Mathf.Infinity;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    
    private Vector2 moveInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.gravityScale = defaultGravityScale;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Jump();
        }

        if (context.canceled && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f);
        }
    }

    private void Update()
    {
        if (moveInput.x > 0.01f)
            transform.localScale = new Vector3(TARGET_SCALE, TARGET_SCALE, 1);
        else if (moveInput.x < -0.01f)
            transform.localScale = new Vector3(-TARGET_SCALE, TARGET_SCALE, 1);
        
        anim.SetBool("run", moveInput.x != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetBool("onWall", onWall());

        cooldownTimer += Time.deltaTime;

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (onWall() && !isGrounded())
        {
            body.gravityScale = defaultGravityScale; 
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Max(body.linearVelocity.y, -wallSlideSpeed));
        }
        else
        {
            body.gravityScale = defaultGravityScale;
            body.linearVelocity = new Vector2(moveInput.x * speed, body.linearVelocity.y);
        }
    }

    private void Jump()
    {        
        if (onWall() && !isGrounded())
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(jumpSound);
            WallJump();
        }
        else if (coyoteCounter > 0)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(jumpSound);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            coyoteCounter = 0; 
        }
        else if (jumpCounter > 0)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(jumpSound);
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            jumpCounter--; 
        }
    }

    private void WallJump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        
        float forceX = -Mathf.Sign(transform.localScale.x) * wallJumpX; 
        body.AddForce(new Vector2(forceX, wallJumpY), ForceMode2D.Impulse);
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        bool isStandingStill = Mathf.Abs(moveInput.x) < 0.01f;
        return isStandingStill && isGrounded() && !onWall() && cooldownTimer > attackCooldown;
    }

    public void ResetAttackCooldown()
    {
        cooldownTimer = 0;
    }
}