using UnityEngine;
using UnityEngine.SceneManagement; // 🌟 Obligatorio para poder cambiar de escena
using UnityEngine.UI;             // 🌟 Obligatorio para controlar la imagen de transición
using System.Collections;         // 🌟 Obligatorio para poder usar Corrutinas (IEnumerator)

public class CambioEscena : MonoBehaviour
{
    [Header("Configuración de Escena")]
    // nombre del siguiente nivel
    public string nombreSiguienteEscena;

    [Header("Configuración de Transición")]
    public Image imagenFondo; // Arrastra aquí la Image negra de tu Canvas "Transicion"
    public float velocidadTransicion = 1.5f; // Qué tan rápido se pone negro

    private bool cambiandoDeEscena = false; // Evita que se ejecute dos veces si el jugador toca el trigger repetidamente

    private void Start()
    {
        // Al iniciar la escena actual, si hay una imagen asignada, hace el Fade In automáticamente
        if (imagenFondo != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si ya está cambiando de escena, no hagas nada
        if (cambiandoDeEscena) return;

        // Detecta si toca la meta el jugador
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            cambiandoDeEscena = true;
            Debug.Log("¡Nivel completado! Iniciando transición para cargar: " + nombreSiguienteEscena);

            // Si tienes la imagen asignada, inicia la transición. Si no, cambia de golpe como antes.
            if (imagenFondo != null)
            {
                StartCoroutine(FadeOutAndLoad());
            }
            else
            {
                SceneManager.LoadScene(nombreSiguienteEscena);
            }
        }
    }

    // Corrutina para poner la pantalla en NEGRO y luego cargar la escena
    private IEnumerator FadeOutAndLoad()
    {
        float alfa = 0;
        imagenFondo.gameObject.SetActive(true);

        while (alfa < 1)
        {
            alfa += Time.deltaTime * velocidadTransicion;
            Color c = imagenFondo.color;
            c.a = Mathf.Clamp01(alfa);
            imagenFondo.color = c;
            yield return null; // Espera al siguiente frame
        }

        // Cambia automáticamente a la escena siguiente una vez esté todo en negro
        SceneManager.LoadScene(nombreSiguienteEscena);
    }

    // Corrutina para APARECER la escena quitando el negro al iniciar
    private IEnumerator FadeIn()
    {
        float alfa = 1;
        imagenFondo.gameObject.SetActive(true);

        // Aseguramos que empiece completamente opaco
        Color cInicio = imagenFondo.color;
        cInicio.a = 1;
        imagenFondo.color = cInicio;

        while (alfa > 0)
        {
            alfa -= Time.deltaTime * velocidadTransicion;
            Color c = imagenFondo.color;
            c.a = Mathf.Clamp01(alfa);
            imagenFondo.color = c;
            yield return null;
        }

        // Desactivamos el objeto para que no bloquee clics en la pantalla
        imagenFondo.gameObject.SetActive(false);
    }
}