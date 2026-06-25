using UnityEngine;

public class PlayerFlight : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerTransform playerTransform;

    private bool insideCurrent = false;

    private Vector2 currentDirection;
    private float currentStrength;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<PlayerTransform>();

        Debug.Log(rb);
        Debug.Log(playerTransform);
    }

    void FixedUpdate()
    {
        if (playerTransform.currentForm != PlayerTransform.Form.Avion)
            return;

        if (!insideCurrent)
            return;

        rb.linearVelocity = currentDirection * currentStrength;
    }

    public void EnterCurrent(Vector2 direction, float strength)
    {
        insideCurrent = true;

        currentDirection = direction;
        currentStrength = strength;
    }

    public void ExitCurrent()
    {
        insideCurrent = false;
    }
}