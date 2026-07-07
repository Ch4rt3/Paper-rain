using UnityEngine;
using UnityEngine.SceneManagement; // Es fundamental incluir esta librería

public class CambiarEscena : MonoBehaviour
{
    [Tooltip("0")]
    public string nombreSiguienteEscena;

    // Este método lo llamará el botón del menú
    public void IniciarJuego()
    {
        Debug.Log("¡El botón funciona!");
        // Verifica que no hayas dejado el campo vacío por error
        if (!string.IsNullOrEmpty(nombreSiguienteEscena))
        {
            SceneManager.LoadScene(nombreSiguienteEscena);
        }
        else
        {
            Debug.LogError("¡Olvidaste arrastrar o escribir el nombre de la escena en el Inspector!");
        }
    }
}