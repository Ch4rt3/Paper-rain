using System.Collections;
using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    private Puerta scriptPuerta;

    void Start()
    {
        // Busca automáticamente el script Puerta en la escena
        scriptPuerta = FindObjectOfType<Puerta>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si lo que choca es la Llave
        if (other.CompareTag("Llave"))
        {
            if (scriptPuerta != null)
            {
                // Le ordena al script de la puerta hacer toda la secuencia
                scriptPuerta.AbrirPuerta();
            }
        }
    }
}