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
        if (!yaSeTransformo)
        {
            TransformarEnRampa();
        }
    }

    private void TransformarEnRampa()
    {
        yaSeTransformo = true;

        if (spriteTriangulo != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = spriteTriangulo;
        }

        if (boxCollider != null) boxCollider.enabled = false;
        if (polygonCollider != null) polygonCollider.enabled = true;
    }
}