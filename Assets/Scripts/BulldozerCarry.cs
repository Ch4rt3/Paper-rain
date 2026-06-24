using UnityEngine;

public class BulldozerCarry : MonoBehaviour
{
    [Header("Detección de empuje")]
    [SerializeField] private float pushCheckDistance = 0.15f;
    [SerializeField] private Vector2 pushCheckSize = new Vector2(0.35f, 0.9f);
    [SerializeField] private float pushSpeed = 2f;

    private Animator animator;
    private PlayerTransform playerTransform;
    private PlayerMove playerMove;
    private Collider2D playerCollider;

    private Rigidbody2D nearbyRb;
    private bool isPushing;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<PlayerTransform>();
        playerMove = GetComponent<PlayerMove>();
        playerCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        if (playerTransform == null || playerCollider == null)
            return;

        if (playerTransform.currentForm != PlayerTransform.Form.Bulldozer)
        {
            SetPushing(false);
            nearbyRb = null;
            return;
        }

        float dir = transform.localScale.x >= 0 ? 1f : -1f;
        Bounds bounds = playerCollider.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x + dir * (bounds.extents.x + pushCheckDistance),
            bounds.center.y
        );

        Collider2D[] hits = Physics2D.OverlapBoxAll(origin, pushCheckSize, 0f);

        nearbyRb = null;
        bool blockInFront = false;

        foreach (Collider2D hit in hits)
        {
            if (hit != null && hit.CompareTag("Pushable"))
            {
                nearbyRb = hit.attachedRigidbody != null ? hit.attachedRigidbody : hit.GetComponent<Rigidbody2D>();
                if (nearbyRb != null)
                {
                    blockInFront = true;
                    break;
                }
            }
        }

        SetPushing(blockInFront);

        if (blockInFront && nearbyRb != null && playerMove != null)
        {
            float inputX = playerMove.CurrentMoveInput.x;

            if (Mathf.Abs(inputX) > 0.01f && Mathf.Sign(inputX) == dir)
            {
                Vector2 targetPos = nearbyRb.position + new Vector2(inputX * pushSpeed * Time.fixedDeltaTime, 0f);
                nearbyRb.MovePosition(targetPos);
            }
        }
    }

    private void SetPushing(bool value)
    {
        if (isPushing == value)
            return;

        isPushing = value;

        if (animator != null)
            animator.SetBool("IsPushing", isPushing);
    }

    private void OnDrawGizmosSelected()
    {
        if (playerCollider == null)
            playerCollider = GetComponent<Collider2D>();

        if (playerCollider == null)
            return;

        float dir = transform.localScale.x >= 0 ? 1f : -1f;
        Bounds bounds = playerCollider.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x + dir * (bounds.extents.x + pushCheckDistance),
            bounds.center.y
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(origin, pushCheckSize);
    }
}