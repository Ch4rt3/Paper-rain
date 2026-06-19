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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Detecta si chocamos con la moto desde arriba para que caiga
        if (collision.gameObject.CompareTag("Obstaculo") && !yaSeCayo)
        {
            yaSeCayo = true;
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; // Cae la manija
            }
            return; 
        }

        // 2. Si la manija cae y choca contra el texto "Obstaculo"
        if (collision.gameObject.name.Contains("TextSave") || collision.gameObject.CompareTag("Obstaculo"))
        {
            // Buscamos el script de la puerta para abrirla visualmente
            PuertaMovimiento movPuerta = Object.FindFirstObjectByType<PuertaMovimiento>();
            if (movPuerta != null)
            {
                movPuerta.EjecutarApertura(); // Activa la animación

                // --- NUEVO: APAGAR EL COLLIDER DE LA PUERTA ---
                // Buscamos el BoxCollider2D que está en el mismo objeto que la puerta
                BoxCollider2D colliderPuerta = movPuerta.GetComponent<BoxCollider2D>();
                if (colliderPuerta != null)
                {
                    colliderPuerta.enabled = false; // ¡Desaparece el muro invisible!
                }
            }

            // Desaparecemos ambos objetos (manija y texto) al instante
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}