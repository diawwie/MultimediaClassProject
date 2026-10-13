using UnityEngine;
using UnityEngine.InputSystem;

public class WalkerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private InputAction moveAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveAction = InputSystem.actions.FindAction("Move");
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
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}
