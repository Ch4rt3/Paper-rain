using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Collections; // 💡 Obligatorio para usar Corrutinas e IEnumerator

public class ControladorIntro : MonoBehaviour
{
    [Header("Paneles de la Historieta")]
    public GameObject[] paneles; // Coloca aquí tus 3 o 5 viñetas en orden de aparición
    
    [Header("Configuración de Tiempo")]
    public float tiempoEspera = 1.0f; // Tiempo en segundos entre cada viñeta

    void Start()
    {
        // 1. Al empezar, apagamos todos los paneles para que solo se vea el fondo sólido
        foreach (GameObject panel in paneles)
        {
            if (panel != null)
            {
                panel.SetActive(false);
            }
        }

        // 2. Iniciamos el temporizador automático
        StartCoroutine(AparecerHistorietaSecuencial());
    }

    // ⏳ Esta función se encarga de esperar 1 segundo entre cada imagen
    IEnumerator AparecerHistorietaSecuencial()
    {
        // Espera un segundo inicial antes de mostrar la primera viñeta
        yield return new WaitForSeconds(tiempoEspera);

        for (int i = 0; i < paneles.Length; i++)
        {
            if (paneles[i] != null)
            {
                // Activamos el panel actual sin apagar los anteriores
                paneles[i].SetActive(true); 
            }

            // Esperamos 1 segundo antes de activar la siguiente viñeta de la lista
            yield return new WaitForSeconds(tiempoEspera);
        }
    }

    // 🚀 Esta función la llamará tu botón "Siguiente" para empezar el juego al instante
    public void IniciarJuego()
    {
        Debug.Log("Saltando introducción / Empezando juego...");
        SceneManager.LoadScene("Level_1"); 
    }
}