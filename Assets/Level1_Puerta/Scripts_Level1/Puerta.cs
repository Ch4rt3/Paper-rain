using UnityEngine;

public class Puerta : MonoBehaviour
{
    // Ahora apuntamos a tu nuevo script de la chimenea
    private TextHistory1 scriptTexto; 
    private bool yaAviso = false;

    void Start()
    {
        // Busca automáticamente el nuevo componente en la escena
        scriptTexto = FindObjectOfType<TextHistory1>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !yaAviso)
        {
            yaAviso = true;
            
            if (scriptTexto != null)
            {
                scriptTexto.RegistrarPrimerChoque(); // Le avisa al nuevo script
            }
        }
    }

    public void AbrirPuertaConAnimacion()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Abrir");
        }
        
        Collider2D colisionador = GetComponent<Collider2D>();
        if (colisionador != null)
        {
            colisionador.enabled = false;
        }
    }
}