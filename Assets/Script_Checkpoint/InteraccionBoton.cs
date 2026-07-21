using UnityEngine;
using System.Collections; // Necesario para usar Corrutinas (el temporizador)

public class InteraccionBoton : MonoBehaviour
{
    [Header("Configuración de Físicas")]
    // Esta variable hace referencia al Rigidbody del tubo en el Inspector
    public Rigidbody2D rigidbodyTubo;

    [Header("Configuración Visual del Botón")]
    // El color que tendrá el botón cuando el jugador lo toque (ej: Rojo Oscuro)
    public Color colorPresionado = new Color(0.5f, 0.0f, 0.0f); 
    // Cuánto tiempo (en segundos) permanece el botón con el color de presionado
    public float tiempoRetornoColor = 0.2f; 

    private SpriteRenderer spriteRendererBoton;
    private Color colorOriginal;
    private bool yaPresionado = false; // Para evitar que se active múltiples veces si el jugador se queda encima

    void Start()
    {
        // Obtenemos el SpriteRenderer del propio objeto Botón
        spriteRendererBoton = GetComponent<SpriteRenderer>();
        
        if (spriteRendererBoton != null)
        {
            // Guardamos el color que tiene al inicio para poder volver a él
            colorOriginal = spriteRendererBoton.color;
        }
        else
        {
            Debug.LogError("¡Cuidado! El script InteraccionBoton está en un objeto sin SpriteRenderer.");
        }
    }

    // Esta función se ejecuta cuando algo (con un Collider 2D) entra en el Trigger del botón
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Comprobamos si lo que tocó el botón es el Jugador y si no lo hemos presionado ya
        if (collision.CompareTag("Player") && !yaPresionado)
        {
            yaPresionado = true; // Marcamos como presionado

            // --- Lógica Visual: Cambiar Color ---
            if (spriteRendererBoton != null)
            {
                // Cambiamos el color al color de "presionado" (ej: rojo oscuro)
                spriteRendererBoton.color = colorPresionado;
                // Iniciamos un temporizador (Corrutina) para que vuelva al color original
                StartCoroutine(RetornarColorOriginal());
            }

            // --- Lógica de Físicas (Tubo): Mismo comportamiento de antes ---
            if (rigidbodyTubo != null)
            {
                // Cambiamos el tipo de cuerpo a Dynamic para que la gravedad actúe
                rigidbodyTubo.bodyType = RigidbodyType2D.Dynamic;
                Debug.Log("¡El botón fue presionado visualmente y el tubo cae!");
            }
        }
    }

    // Esta función es un temporizador que espera un momento y luego restaura el color
    IEnumerator RetornarColorOriginal()
    {
        // Esperamos el tiempo configurado (ej: 0.2 segundos)
        yield return new WaitForSeconds(tiempoRetornoColor);
        
        // Restauramos el color original
        if (spriteRendererBoton != null)
        {
            spriteRendererBoton.color = colorOriginal;
        }
        
        // Opcional: Permitir que se presione de nuevo si el jugador se va y vuelve
        // yaPresionado = false; 
    }
}