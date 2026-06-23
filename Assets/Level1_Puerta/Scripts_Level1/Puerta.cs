using UnityEngine;

public class Puerta : MonoBehaviour
{
    [Header("Configuración Visual")]
    public Sprite spritePuertaAbierta; 

    private TextHistory1 scriptTexto;
    private bool yaAviso = false;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D colisionador;

    void Start()
    {
        
        scriptTexto = FindObjectOfType<TextHistory1>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisionador = GetComponent<BoxCollider2D>();
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !yaAviso)
        {
            yaAviso = true;

            if (scriptTexto != null)
            {
                
                scriptTexto.RegistrarPrimerChoque();
                Debug.Log("Puerta: ¡Primer golpe registrado! Historia activada.");
            }
        }
    }

    
    public void AbrirPuertaDirecto()
    {
        if (spriteRenderer != null && spritePuertaAbierta != null)
        {
            spriteRenderer.sprite = spritePuertaAbierta;
        }

        if (colisionador != null)
        {
            colisionador.enabled = false; // Desaparece el muro para dejar pasar la moto
        }
        Debug.Log("Puerta: Abierta automáticamente desde el sensor.");
    }
}