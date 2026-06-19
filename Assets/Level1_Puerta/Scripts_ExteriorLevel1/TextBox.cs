using UnityEngine;
using TMPro;
using System.Collections;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI textoComponente;
    private BoxCollider2D colliderFisico; 
    public float velocidadFade = 1f;
    
    [Header("Ajustes de Elevación")]
    public float fuerzaDeEmpuje = 5f; // Fuerza para levantar la caja hacia arriba

    void Start()
    {
        textoComponente = GetComponentInChildren<TextMeshProUGUI>();
        colliderFisico = GetComponent<BoxCollider2D>();

        if (textoComponente != null)
        {
            Color c = textoComponente.color;
            c.a = 0f;
            textoComponente.color = c;
        }
        
        if (colliderFisico != null)
        {
            colliderFisico.enabled = false; 
        }
    }

    public void ActivarTextoYPlataforma()
    {
        StartCoroutine(Aparecer());
    }

    private IEnumerator Aparecer()
    {
        if (textoComponente == null) yield break;

        Color c = textoComponente.color;
        while (c.a < 1f)
        {
            c.a += Time.deltaTime * velocidadFade;
            textoComponente.color = c;
            yield return null;
        }

        // 1. Activamos el colisionador sólido del texto abajo de la caja
        if (colliderFisico != null)
        {
            colliderFisico.enabled = true;
        }

        // 2. TRUCO FÍSICO: Buscamos si la caja está encima y le damos un impulso hacia arriba
        // para evitar que se quede atorada o traspase el texto.
        Rigidbody2D rbCaja = Object.FindFirstObjectByType<Rigidbody2D>(); 
        // Nota: Si tienes más rigidbodies, es mejor buscarlo por su Tag:
        // GameObject objetoCaja = GameObject.FindWithTag("Caja");
        // if (objetoCaja != null) { Rigidbody2D rbCaja = objetoCaja.GetComponent<Rigidbody2D>(); ... }

        if (rbCaja != null && rbCaja.CompareTag("Caja"))
        {
            // Le aplicamos una velocidad inmediata hacia arriba en el eje Y
            rbCaja.linearVelocity = new Vector2(rbCaja.linearVelocity.x, fuerzaDeEmpuje);
        }
    }
}