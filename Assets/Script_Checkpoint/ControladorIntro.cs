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
        // 1. Apagamos absolutamente todas las viñetas al iniciar
        foreach (GameObject vineta in vinetas)
        {
            if (vineta != null) vineta.SetActive(false);
        }

        // 2. Mostramos el primer grupo de 4 imágenes de forma secuencial
        MostrarSiguienteGrupo();
    }

    // Esta función maneja la lógica de agrupar de 4 en 4
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

        // Apagamos las 4 imágenes anteriores para limpiar la pantalla antes del nuevo grupo
        // (Buscamos las 4 que acabamos de pasar)
        int inicioAnterior = indiceActual - 4;
        for (int i = inicioAnterior; i < indiceActual; i++)
        {
            if (i >= 0 && i < vinetas.Length && vinetas[i] != null)
            {
                vinetas[i].SetActive(false);
            }
        }

        // Iniciamos la secuencia para el nuevo bloque de 4 imágenes
        MostrarSiguienteGrupo();
    }

    private void MostrarSiguienteGrupo()
    {
        corutinaActual = StartCoroutine(AparecerGrupoSecuencial());
    }

    IEnumerator AparecerGrupoSecuencial()
    {
        // Calculamos hasta dónde debe llegar este grupo (máximo 4 imágenes o el final de la lista)
        int limiteGrupo = indiceActual + 4;

        while (indiceActual < limiteGrupo && indiceActual < vinetas.Length)
        {
            if (vinetas[indiceActual] != null)
            {
                vinetas[indiceActual].SetActive(true);
            }

            indiceActual++; // Avanzamos el contador global

            // Esperamos 2 segundos antes de que aparezca la siguiente del mismo grupo
            // Solo esperamos si no es la última imagen del grupo actual
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