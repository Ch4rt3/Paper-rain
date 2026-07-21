using UnityEngine;

public class InteraccionBoton : MonoBehaviour
{
    // Esta variable hará referencia al Rigidbody del tubo en el Inspector
    public Rigidbody2D rigidbodyTubo;

    // Esta función se ejecuta cuando algo (con un Collider 2D) entra en el Trigger del botón
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si lo que tocó el botón es el Jugador
        if (collision.CompareTag("Player"))
        {
            // Si el tubo tiene Rigidbody2D
            if (rigidbodyTubo != null)
            {
                // Cambiamos el tipo de cuerpo a Dynamic para que la gravedad actúe
                rigidbodyTubo.bodyType = RigidbodyType2D.Dynamic;
                
                // Si ya era Dynamic pero con Gravity Scale 0, cámbialo aquí:
                // rigidbodyTubo.gravityScale = 1;
                
                Debug.Log("¡El botón fue presionado, el tubo cae!");
            }
        }
    }
}