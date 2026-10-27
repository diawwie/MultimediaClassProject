using UnityEngine;
using UnityEngine.InputSystem;

public class WalkerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] private float jumpVelocity = 15f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float coyoteTime = 0.1f;
    private float coyoteTimer;

    private InputAction jumpAction;
    private bool jumpRequested;
    private bool jumpCutRequested;
    private bool isGrounded;

    private static readonly int SpeedHash    = Animator.StringToHash("Speed");
    private static readonly int GroundedHash = Animator.StringToHash("IsGrounded");

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private InputAction moveAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        Debug.Log($"Awake: rb={rb != null}, sr={spriteRenderer != null}, " +
                  $"move={moveAction != null}");
    }

    private float moveInput;

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>().x;
        if (moveInput != 0f)
        {
            spriteRenderer.flipX = moveInput > 0f;
        }

        if (jumpAction.WasPressedThisFrame())
        {
            jumpRequested = true;
        }

        if (jumpAction.WasReleasedThisFrame())
        {
            jumpCutRequested = true;
        }

        animator.SetFloat(SpeedHash, Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool(GroundedHash, isGrounded);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayer) != null;

        if (isGrounded) coyoteTimer = coyoteTime;
        else            coyoteTimer -= Time.fixedDeltaTime;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (jumpRequested && coyoteTimer > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            coyoteTimer = 0f;   // a granted jump spends the whole window
        }

        if (jumpCutRequested && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        jumpRequested = false;
        jumpCutRequested = false;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
