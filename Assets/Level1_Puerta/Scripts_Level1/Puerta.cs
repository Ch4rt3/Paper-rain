using System.Collections;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    private Animator animator;
    private Collider2D puertaCollider; // 🌟 Nueva variable para el colisionador

    [Header("Objetos a Desaparecer")]
    public GameObject objetoLlave;    
    public GameObject objetoManija;   

    void Start()
    {
        animator = GetComponent<Animator>();
        puertaCollider = GetComponent<Collider2D>(); // 🌟 Encuentra el Collider automático
    }

    public void AbrirPuerta()
    {
        // 1. Desaparece la llave y la manija de inmediato
        if (objetoLlave != null) objetoLlave.SetActive(false);
        if (objetoManija != null) objetoManija.SetActive(false);

        // 🌟 2. APAGA EL MURO INVISIBLE: Desactiva el colisionador de la puerta
        if (puertaCollider != null)
        {
            puertaCollider.enabled = false;
        }

        // 3. Activa tu animación original de apertura
        if (animator != null)
        {
            animator.SetTrigger("Abrir");
        }
    }
}