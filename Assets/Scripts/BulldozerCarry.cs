using UnityEngine;

public class BulldozerCarry : MonoBehaviour
{
    public Transform carryPoint;

    private GameObject nearbyBlock;
    private GameObject carriedBlock;

    private Rigidbody2D carriedRb;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("LiftBlade");

            if (carriedBlock == null)
                PickUp();
            else
                Drop();
        }
    }

    void PickUp()
    {
        if (nearbyBlock == null)
            return;

        carriedBlock = nearbyBlock;
        carriedRb = carriedBlock.GetComponent<Rigidbody2D>();

        if (carriedRb != null)
        {
            carriedRb.linearVelocity = Vector2.zero;
            carriedRb.angularVelocity = 0f;
            carriedRb.bodyType = RigidbodyType2D.Kinematic;
        }

        carriedBlock.transform.SetParent(carryPoint);
        carriedBlock.transform.localPosition = Vector3.zero;
    }

    void Drop()
    {
        if (carriedRb != null)
        {
            carriedRb.bodyType = RigidbodyType2D.Dynamic;
        }

        carriedBlock.transform.SetParent(null);

        carriedBlock = null;
        carriedRb = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            nearbyBlock = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pushable"))
        {
            nearbyBlock = null;
        }
    }
}