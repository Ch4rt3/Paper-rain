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

    // CAMBIADO A TRIGGER: Evita que los objetos se queden trabados en el aire
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Si el objeto físico que cae tiene el Tag "Obstaculo"
        if (other.CompareTag("Obstaculo") && !yaSeCayo)
        {
            yaSeCayo = true;
            
            // Le quitamos el modo "Is Trigger" para que cuando caiga al suelo SÍ choque de verdad
            Collider2D miCollider = GetComponent<Collider2D>();
            if (miCollider != null) miCollider.isTrigger = false;

            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; // ¡Cae por gravedad de inmediato!
            }
        }
    }

    // Detecta cuando la manija toca el suelo real
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Piso") || collision.gameObject.CompareTag("Suelo"))
        {
            TextSave1 textoObstaculo = Object.FindFirstObjectByType<TextSave1>();
            if (textoObstaculo != null)
            {
                textoObstaculo.DesaparecerObjetosYAbrirPuerta(this.gameObject);
            }
        }
    }
}