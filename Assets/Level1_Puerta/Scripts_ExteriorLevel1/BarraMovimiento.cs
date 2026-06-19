using UnityEngine;

public class BarraMovimiento : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Al iniciar el nivel, la barra se mantiene fija flotando en el aire
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // Esta función pública la llamará el sensor desde el suelo
    public void ActivarCaidaDeBarra()
    {
        if (rb != null)
        {
            // Resetea cualquier velocidad acumulada y activa la gravedad real (Dynamic)
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 1f;
            rb.bodyType = RigidbodyType2D.Dynamic; 
            Debug.Log("¡La barra de metal ha comenzado a caer físicamente!");
        }
    }
}