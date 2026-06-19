using UnityEngine;
using TMPro;
using System.Collections;

public class ObstaculoEfecto : MonoBehaviour
{
    private TextMeshProUGUI textoTMP; 
    private bool yaEnSuelo = false;

    void Start()
    {
        textoTMP = GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Inicio el desvanecimiento
        if (collision.gameObject.CompareTag("Suelo") && !yaEnSuelo)
        {
            yaEnSuelo = true;
            StartCoroutine(DesvanecerYDestruir());
        }
    }

    private IEnumerator DesvanecerYDestruir()
    {
        yield return new WaitForSeconds(0.5f);

        float tiempoDesvanecimiento = 0.5f; 
        float tiempoTranscurrido = 0f;
        
      
        Color colorOriginal = textoTMP.color;

        while (tiempoTranscurrido < tiempoDesvanecimiento)
        {
            tiempoTranscurrido += Time.deltaTime;
            float nuevoAlfa = Mathf.Lerp(0.5f, 0f, tiempoTranscurrido / tiempoDesvanecimiento);
            
            textoTMP.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, nuevoAlfa);
            yield return null;
        }

    
        Destroy(gameObject);
    }
}   