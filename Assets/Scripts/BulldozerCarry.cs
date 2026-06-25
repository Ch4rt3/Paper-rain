using System.Collections;
using UnityEngine;

public class BulldozerCarry : MonoBehaviour
{
    [Header("Detección de empuje")]
    [SerializeField] private float pushCheckDistance = 0.15f;
    [SerializeField] private Vector2 pushCheckSize = new Vector2(0.35f, 0.9f);
    [SerializeField] private float pushSpeed = 2f;

    [Header("Empujón con E")]
    [SerializeField] private float bladePushDistance = 1.5f;
    [SerializeField] private float bladePushSpeed = 3f;

    private Animator animator;
    private PlayerTransform playerTransform;
    private PlayerMove playerMove;
    private Collider2D playerCollider;

    private Rigidbody2D nearbyRb;
    private bool isPushing;
    private bool isBladeAnimationPlaying;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<PlayerTransform>();
        playerMove = GetComponent<PlayerMove>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (playerTransform.currentForm != PlayerTransform.Form.Bulldozer)
            return;

        if (Input.GetKeyDown(KeyCode.E) && !isBladeAnimationPlaying)
        {
            StartCoroutine(BladePushRoutine());
        }
    }

    IEnumerator BladePushRoutine()
    {
        isBladeAnimationPlaying = true;

        animator.SetTrigger("LiftBlade");

        if (nearbyRb != null)
        {
            float dir = transform.localScale.x >= 0 ? 1f : -1f;

            StartCoroutine(
                PushBoxSmooth(
                    nearbyRb,
                    dir,
                    bladePushDistance,
                    bladePushSpeed
                )
            );
        }

        yield return new WaitForSeconds(0.35f);

        isBladeAnimationPlaying = false;
    }

    IEnumerator PushBoxSmooth(
        Rigidbody2D rb,
        float dir,
        float distance,
        float speed)
    {
        Vector2 start = rb.position;
        Vector2 end = start + new Vector2(dir * distance, 0f);

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speed;

            rb.MovePosition(
                Vector2.Lerp(start, end, t)
            );

            yield return null;
        }

        rb.MovePosition(end);
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

        if (isBladeAnimationPlaying)
        {
            SetPushing(false);
            return;
        }

        float dir = transform.localScale.x >= 0 ? 1f : -1f;

        Bounds bounds = playerCollider.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x + dir * (bounds.extents.x + pushCheckDistance),
            bounds.center.y
        );

        Collider2D[] hits = Physics2D.OverlapBoxAll(
            origin,
            pushCheckSize,
            0f
        );

        nearbyRb = null;
        bool blockInFront = false;

        foreach (Collider2D hit in hits)
        {
            if (hit != null && hit.CompareTag("Pushable"))
            {
                nearbyRb = hit.attachedRigidbody != null
                    ? hit.attachedRigidbody
                    : hit.GetComponent<Rigidbody2D>();

                if (nearbyRb != null)
                {
                    blockInFront = true;
                    break;
                }
            }
        }

        bool actuallyPushing = false;

        if (blockInFront && nearbyRb != null && playerMove != null)
        {
            float inputX = playerMove.CurrentMoveInput.x;

            if (Mathf.Abs(inputX) > 0.01f &&
                Mathf.Sign(inputX) == dir)
            {
                actuallyPushing = true;

                Vector2 targetPos = nearbyRb.position +
                    new Vector2(
                        inputX * pushSpeed * Time.fixedDeltaTime,
                        0f
                    );

                nearbyRb.MovePosition(targetPos);
            }
        }

        SetPushing(actuallyPushing);
    }

    private void SetPushing(bool value)
    {
        if (isPushing == value)
            return;

        isPushing = value;

        if (animator != null)
        {
            animator.SetBool("IsPushing", isPushing);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Collider2D col = GetComponent<Collider2D>();

        if (col == null)
            return;

        float dir = transform.localScale.x >= 0 ? 1f : -1f;

        Bounds bounds = col.bounds;

        Vector2 origin = new Vector2(
            bounds.center.x + dir * (bounds.extents.x + pushCheckDistance),
            bounds.center.y
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(origin, pushCheckSize);
    }
}