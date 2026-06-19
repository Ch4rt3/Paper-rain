using UnityEngine;

public class SensorBarra : MonoBehaviour
{
    private bool yaSeActivo = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // CORREGIDO: Evaluamos si el objeto es la caja Y si aún NO se ha activado (!yaSeActivo)
        if ((other.gameObject.name.Contains("Caja") || other.CompareTag("Obstaculo")) && !yaSeActivo)
        {
            yaSeActivo = true; // Marcamos como activado para que no se repita

            // Busca el script exclusivo de la barra en la escena y activa su caída
            BarraMovimiento scriptBarra = Object.FindFirstObjectByType<BarraMovimiento>();
            if (scriptBarra != null)
            {
                scriptBarra.ActivarCaidaDeBarra();
            }
            else
            {
                Debug.LogError("No se encontró el script 'BarraMovimiento' en ningún objeto de la escena.");
            }
        }
    }
}