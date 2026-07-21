using UnityEngine;
using UnityEngine.SceneManagement; // Necesario si quieres reiniciar la escena al morir

public class AplastamientoTubo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificamos si el objeto con el que chocó el tubo tiene la etiqueta "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El tubo aplastó al jugador!");

            // Opción A: Reiniciar la escena actual de inmediato
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            // Opción B: Si tienes un script de salud/muerte en el jugador, puedes llamarlo aquí:
            // collision.gameObject.GetComponent<ControladorJugador>().Morir();
        }
    }
}