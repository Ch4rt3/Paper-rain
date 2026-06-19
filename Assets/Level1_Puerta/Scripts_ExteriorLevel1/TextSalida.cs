using UnityEngine;
using TMPro; // Asegúrate de incluir esto si usas TextMeshPro
using System.Collections;

public class TextSalida : MonoBehaviour
{
    private TextMeshProUGUI textoComponente; // Cambia a 'Text' si usas el sistema antiguo
    public float velocidadFade = 1f;

    void Start()
    {
        textoComponente = GetComponent<TextMeshProUGUI>();
        
        // Empezamos con el texto invisible (Alpha en 0)
        if (textoComponente != null)
        {
            Color c = textoComponente.color;
            c.a = 0f;
            textoComponente.color = c;
        }
    }

    // Este es el método que llamará el sensor del muro
    public void MostrarTextoSuave()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        if (textoComponente == null) yield break;

        Color c = textoComponente.color;
        while (c.a < 1f)
        {
            c.a += Time.deltaTime * velocidadFade;
            textoComponente.color = c;
            yield return null;
        }
    }
}