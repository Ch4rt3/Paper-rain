using System.Collections;
using UnityEngine;

public class SensorTriggerCentro : MonoBehaviour
{
    private TextHistory1 scriptTexto;

    [Header("Referencias de la Rampa")]
    public Animator mesaAnimator; 

    private bool yaSeActivo = false;

    void Start()
    {
        scriptTexto = FindObjectOfType<TextHistory1>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !yaSeActivo)
        {
            yaSeActivo = true;
            StartCoroutine(SecuenciaMesaRampa());
        }
    }

    private IEnumerator SecuenciaMesaRampa()
    {
        // 1. El texto aparece de inmediato al chocar
        if (scriptTexto != null)
        {
            scriptTexto.RegistrarPrimerChoque(); 
            scriptTexto.MostrarTextoSuave();    
        }

        // ========================================================
        // 🌟 AL INSTANTE: Quitamos el "yield return" de espera
        // ========================================================

        // 2. Le da la orden al libro de caer inmediatamente
        if (mesaAnimator != null)
        {
            mesaAnimator.SetTrigger("Caer"); 
        }

        yield return null; // Requisito obligatorio para mantener la corrutina activa
    }
}