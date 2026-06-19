using UnityEngine;

public class BulldozerCarry : MonoBehaviour
{
    public float pushForce = 1f;

    private GameObject nearbyBlock;
    private Rigidbody2D nearbyRb;

    private Animator animator;
    private PlayerTransform playerTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<PlayerTransform>();
    }

    void Update()
    {
        if (playerTransform.currentForm != PlayerTransform.Form.Bulldozer)
            return;

        if (Input.GetKeyDown(KeyCode.E) && nearbyRb != null)
        {
            animator.SetTrigger("LiftBlade");

            float direction = transform.localScale.x > 0 ? 1f : -1f;

            nearbyRb.AddForce(
                new Vector2(direction * pushForce, 0f),
                ForceMode2D.Impulse
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            nearbyBlock = other.gameObject;
            nearbyRb = other.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            nearbyBlock = null;
            nearbyRb = null;
        }
    }
}