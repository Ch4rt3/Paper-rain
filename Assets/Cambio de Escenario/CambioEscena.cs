using UnityEngine;
using UnityEngine.SceneManagement; // 🌟 Obligatorio para poder cambiar de escena

public class CambioEscena : MonoBehaviour
{
    // nombre del siguiente nivel
    public string nombreSiguienteEscena;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si toca la meta el jugador
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡Nivel completado! Cargando: " + nombreSiguienteEscena);
            
            // Cambia automáticamente a la escena siguiente
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}