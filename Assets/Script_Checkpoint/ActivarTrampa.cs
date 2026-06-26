using UnityEngine;

public class ActivarTrampa : MonoBehaviour
{
    
    public Rigidbody2D manzanaRigidbody; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Moto pasa por el sensor
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            if (manzanaRigidbody != null)
            {
                manzanaRigidbody.gravityScale = 2f; // Activa la gravedad
                Debug.Log("¡Trampa activada! Cayendo manzana...");
            }
        }
    }
}
