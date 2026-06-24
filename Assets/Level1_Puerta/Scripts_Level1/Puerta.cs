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
        if (objetoLlave != null) objetoLlave.SetActive(false);
        if (objetoManija != null) objetoManija.SetActive(false);

        if (puertaCollider != null)
        {
            puertaCollider.enabled = false; 
        }

        if (animator != null)
        {
            animator.SetTrigger("Abrir");
        }
    }
}