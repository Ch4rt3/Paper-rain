using UnityEngine;
using TMPro; // ¡IMPORTANTE! Añade esto para poder controlar el texto
using System.Collections;

public class ObstaculoEfecto : MonoBehaviour
{
    private TextMeshProUGUI textoTMP; // Cambiado de SpriteRenderer a TextMeshProUGUI
    private bool yaEnSuelo = false;

    void Start()
    {
        // Buscamos el componente de texto en el objeto
        textoTMP = GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Al tocar el suelo, inicia el desvanecimiento
        if (collision.gameObject.CompareTag("Suelo") && !yaEnSuelo)
        {
            yaEnSuelo = true;
            StartCoroutine(DesvanecerYDestruir());
        }
    }

    private IEnumerator DesvanecerYDestruir()
    {
        // Espera 2 segundos antes de empezar a borrarse
        yield return new WaitForSeconds(2f);

        float tiempoDesvanecimiento = 1f; 
        float tiempoTranscurrido = 0f;
        
        // Obtenemos el color original del texto
        Color colorOriginal = textoTMP.color;

        while (tiempoTranscurrido < tiempoDesvanecimiento)
        {
            tiempoTranscurrido += Time.deltaTime;
            float nuevoAlfa = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoDesvanecimiento);
            
            // Aplicamos el nuevo canal alfa al texto
            textoTMP.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, nuevoAlfa);
            yield return null;
        }

        // Lo borramos de la escena
        Destroy(gameObject);
    }
}