using UnityEngine;
using System.Collections;

public class Puerta : MonoBehaviour
{
    private Animator animator;
    private Collider2D colisionador;
    private SpriteRenderer spriteRenderer;

    [Header("Configuración de la Puerta Abierta")]
    // Aquí arrastraremos desde Unity el sprite de la puerta abierta de par en par
    public Sprite spritePuertaAbierta; 

    void Start()
    {
        animator = GetComponent<Animator>();
        colisionador = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DesaparecerPuerta()
    {
        // 1. Desactivamos el colisionador para que la moto pase libremente
        if (colisionador != null)
        {
            colisionador.enabled = false;
        }

        // 2. Activamos la animación de apertura
        if (animator != null)
        {
            animator.SetTrigger("Abrir");
            StartCoroutine(ForzarPuertaAbierta());
        }
    }

    private IEnumerator ForzarPuertaAbierta()
    {
        // Esperamos exactamente 0.3 segundos a que termine la animación de abrirse
        yield return new WaitForSeconds(0.3f); 

        // 3. Apagamos el Animator para que deje de controlar la imagen
        if (animator != null)
        {
            animator.enabled = false;
        }

        // 4. Forzamos visualmente a que se muestre el sprite de la puerta abierta
        if (spriteRenderer != null && spritePuertaAbierta != null)
        {
            spriteRenderer.sprite = spritePuertaAbierta;
        }
    }
}