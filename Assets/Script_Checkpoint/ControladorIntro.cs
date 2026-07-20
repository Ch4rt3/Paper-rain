using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorIntro : MonoBehaviour
{
    [Header("Todas las Viñetas Individuales")]
    [Tooltip("Coloca aquí absolutamente todas las imágenes en orden (C1_1, C1_2, etc.)")]
    public GameObject[] vinetas;

    [Header("Configuración de Tiempo")]
    public float tiempoEspera = 2.0f; // 2 segundos entre cada imagen

    [Header("Nombre de la Escena a Cargar")]
    public string nombreSiguienteEscena = "Level_1";

    private int indiceActual = 0; // Controla qué imagen toca activar
    private Coroutine corutinaActual;

    void Start()
    {
        // 1. Apagar todas las imagenes al iniciar
        foreach (GameObject vineta in vinetas)
        {
            if (vineta != null) vineta.SetActive(false);
        }

        // 2. Mostramos las primeras 4 imágenes
        MostrarSiguienteGrupo();
    }

   
    public void AvanzarCinematica()
    {
        // Si la corutina del grupo anterior sigue corriendo, la detenemos para evitar conflictos
        if (corutinaActual != null)
        {
            StopCoroutine(corutinaActual);
        }

        // Si ya no quedan más imágenes en la lista completa, pasamos al nivel
        if (indiceActual >= vinetas.Length)
        {
            IniciarJuego();
            return;
        }

        // Apagar las 4 imágenes anteriores
        // Nuevas 4 imagenes
        int inicioAnterior = indiceActual - 4;
        for (int i = inicioAnterior; i < indiceActual; i++)
        {
            if (i >= 0 && i < vinetas.Length && vinetas[i] != null)
            {
                vinetas[i].SetActive(false);
            }
        }

        // Iniciar el nuevo bloque
        MostrarSiguienteGrupo();
    }

    private void MostrarSiguienteGrupo()
    {
        corutinaActual = StartCoroutine(AparecerGrupoSecuencial());
    }

    IEnumerator AparecerGrupoSecuencial()
    {
        // Maximo 4 imagenes)
        int limiteGrupo = indiceActual + 4;

        while (indiceActual < limiteGrupo && indiceActual < vinetas.Length)
        {
            if (vinetas[indiceActual] != null)
            {
                vinetas[indiceActual].SetActive(true);
            }

            indiceActual++; 

            // Esperar 2 segundos
            
            if (indiceActual < limiteGrupo && indiceActual < vinetas.Length)
            {
                yield return new WaitForSeconds(tiempoEspera);
            }
        }
    }

    public void IniciarJuego()
    {
        Debug.Log("Saltando introducción / Empezando juego...");
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}