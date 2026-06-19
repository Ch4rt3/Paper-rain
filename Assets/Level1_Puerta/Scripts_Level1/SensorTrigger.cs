using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    private TextSave1 scriptTexto;
    private bool yaPaso = false;

    void Start()
    {
        scriptTexto = FindObjectOfType<TextSave1>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Al pisar el trigger, le avisa al texto. El texto decidirá si el candado está abierto o cerrado.
        if (other.CompareTag("Player") && !yaPaso && scriptTexto != null)
        {
            // Ojo: No volvemos 'yaPaso = true' aquí para permitir que el script intente activarse 
            // cuando sea el momento correcto de la historia.
            
            // Buscamos si el candado de la chimenea ya se abrió
            TextHistory1 textoChimenea = FindObjectOfType<TextHistory1>();
            if (textoChimenea != null && textoChimenea.VerificarSiPoemaFueLeido())
            {
                yaPaso = true; // Solo se quema el sensor si la secuencia realmente se dispara
            }

            scriptTexto.DispararSecuenciaFinal(); 
        }
    }
}