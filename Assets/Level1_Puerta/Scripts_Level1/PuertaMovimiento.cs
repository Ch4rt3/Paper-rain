using UnityEngine;

public class PuertaMovimiento : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Consigue el Animator del mismo objeto donde esté pegado
        animator = GetComponent<Animator>();
    }

    // Esta función pública la llamará la manija al tocar el piso
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