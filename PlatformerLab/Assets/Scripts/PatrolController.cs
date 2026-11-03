using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2.5f;

    [Header("Sensors")]
    [SerializeField] private Vector2 edgeCheckOffset = new Vector2(0.45f, 0.1f);
    [SerializeField] private float edgeCheckDistance = 0.3f;
    [SerializeField] private Vector2 wallCheckOffset = new Vector2(0.45f, 0.45f);
    [SerializeField] private float wallCheckDistance = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private int direction = 1;

    private Vector2 EdgeOrigin => (Vector2)transform.position
        + new Vector2(edgeCheckOffset.x * direction, edgeCheckOffset.y);
    private Vector2 WallOrigin => (Vector2)transform.position
        + new Vector2(wallCheckOffset.x * direction, wallCheckOffset.y);

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = direction > 0;   // the frames face left; it starts walking right
    }

    void FixedUpdate()
    {
        bool groundAhead = Physics2D.Raycast(
            EdgeOrigin, Vector2.down, edgeCheckDistance, groundLayer).collider != null;
        bool wallAhead = Physics2D.Raycast(
            WallOrigin, Vector2.right * direction, wallCheckDistance, groundLayer).collider != null;

        if (!groundAhead || wallAhead)
        {
            Flip();
        }

        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        direction *= -1;
        spriteRenderer.flipX = direction > 0;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(EdgeOrigin, EdgeOrigin + Vector2.down * edgeCheckDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(WallOrigin, WallOrigin + Vector2.right * direction * wallCheckDistance);
    }
}