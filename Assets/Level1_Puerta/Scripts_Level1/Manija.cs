using UnityEngine;

public class Manija : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool yaSeCayo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic; // Quieta al inicio
        }
    }

    public void ActivarCaidaDeManija()
    {
        if (rb != null && !yaSeCayo)
        {
            yaSeCayo = true;
            rb.bodyType = RigidbodyType2D.Dynamic; // Cae por gravedad
        }
    }
}