using UnityEngine;

public class SpriteTriangulo : MonoBehaviour 
{
    [Header("=== TRANSFORMACIÓN ===")]  
    [Tooltip("sprite del triángulo blanco.")]
    public Sprite spriteTriangulo;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private PolygonCollider2D polygonCollider;
    private Rigidbody2D rb;
    private bool yaSeTransformo = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        polygonCollider = GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Evitamos que se transforme si choca con la moto (Player), queremos que lo haga al tocar el suelo
        if (!yaSeTransformo && !collision.gameObject.CompareTag("Player"))
        {
            TransformarEnRampa();
        }
    }

    private void TransformarEnRampa()
    {
        yaSeTransformo = true;

        // 1. Cambiar el aspecto visual al triángulo
        if (spriteTriangulo != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = spriteTriangulo;
        }

        // 2. Apagar la caja física y activar el triángulo físico
        if (boxCollider != null) boxCollider.enabled = false;
        if (polygonCollider != null) polygonCollider.enabled = true;

        // 3. ANCLAR LA RAMPA (La vuelve totalmente inmóvil para que la moto suba)
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Detiene el impacto inicial de la caída
            rb.angularVelocity = 0f;          // Evita cualquier rotación
            
            // Bloquea por completo el movimiento en X, Y y la rotación nativa de Unity
            rb.constraints = RigidbodyConstraints2D.FreezeAll; 
        }
    }
}