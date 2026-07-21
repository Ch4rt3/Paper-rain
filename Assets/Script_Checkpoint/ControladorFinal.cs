using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorFinal : MonoBehaviour
{
    [Header("Todas las Viñetas Individuales")]
    [Tooltip("Coloca aquí absolutamente todas las imágenes en orden (C1_1, C1_2, etc.)")]
    public GameObject[] vinetas;

    [Header("Configuración de Tiempo")]
    public float tiempoEspera = 2.0f; // 2 segundos entre cada imagen

    [Header("Nombre de la Escena a Cargar")]
    public string nombreSiguienteEscena = "MainMenu"; // Nombre por defecto para ir al menú principal

    private int paginaActual = 0; // Controla en qué grupo de 4 estamos
    private Coroutine corutinaActual;

    void Start()
    {
        // 1. Apagar todas las imagenes al iniciar
        foreach (GameObject vineta in vinetas)
        {
            if (vineta != null) vineta.SetActive(false);
        }

        // 2. Mostramos el primer grupo
        paginaActual = 0;
        MostrarSiguienteGrupo();
    }
   
    public void AvanzarCinematica()
    {
        // Si la corutina del grupo anterior sigue corriendo, la detenemos
        if (corutinaActual != null)
        {
            StopCoroutine(corutinaActual);
        }

        // Apagar el grupo actual (las 4 imágenes de la página actual)
        int inicioActual = paginaActual * 4;
        for (int i = inicioActual; i < inicioActual + 4; i++)
        {
            if (i >= 0 && i < vinetas.Length && vinetas[i] != null)
            {
                vinetas[i].SetActive(false);
            }
        }

        // Avanzar a la siguiente página
        paginaActual++;

        // Si ya no quedan más páginas, pasamos a la siguiente escena (menú)
        if (paginaActual * 4 >= vinetas.Length)
        {
            TerminarCinematica();
            return;
        }

        // Iniciar el nuevo bloque de 4
        MostrarSiguienteGrupo();
    }

    private void MostrarSiguienteGrupo()
    {
        corutinaActual = StartCoroutine(AparecerGrupoSecuencial());
    }

    IEnumerator AparecerGrupoSecuencial()
    {
        int inicio = paginaActual * 4;
        int limiteGrupo = inicio + 4;

        for (int i = inicio; i < limiteGrupo && i < vinetas.Length; i++)
        {
            if (vinetas[i] != null)
            {
                vinetas[i].SetActive(true);
            }
            
            // Esperar antes de mostrar la siguiente imagen (excepto si es la última del grupo)
            if (i + 1 < limiteGrupo && i + 1 < vinetas.Length)
            {
                yield return new WaitForSeconds(tiempoEspera);
            }
        }
    }

    public void TerminarCinematica()
    {
        Debug.Log("Terminando cinemática final / Cargando escena: " + nombreSiguienteEscena);
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}
