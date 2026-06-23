using UnityEngine;

public class PuertaMovimiento : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    
    public void EjecutarApertura()
    {
        if (animator != null)
        {
            animator.SetTrigger("Abrir"); 
            Debug.Log("¡PuertaMovimiento activó el trigger 'Abrir' exitosamente!");
        }
        else
        {
            Debug.LogError("No se encontró un Animator en este objeto para PuertaMovimiento.");
        }
    }
}