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
        // 1. Detecta el golpe sólido del objeto con el Tag "Obstaculo"
        if (collision.gameObject.CompareTag("Obstaculo") && !yaSeCayo)
        {
            yaSeCayo = true;
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic; // Cae la manija por gravedad
            }
        }

        // 2. Cuando la manija cae y choca contra el suelo (Piso o Suelo)
        if (collision.gameObject.name.Contains("Piso") || collision.gameObject.CompareTag("Suelo"))
        {
            // NUEVO: Busca el script independiente y activa el movimiento de la puerta
            PuertaMovimiento movPuerta = Object.FindFirstObjectByType<PuertaMovimiento>();
            if (movPuerta != null)
            {
                movPuerta.EjecutarApertura();
            }

            // Tu código original intacto que limpia los objetos de la escena
            TextSave1 textoObstaculo = Object.FindFirstObjectByType<TextSave1>();
            if (textoObstaculo != null)
            {
                textoObstaculo.DesaparecerObjetosYAbrirPuerta(this.gameObject);
            }
        }
    }
}