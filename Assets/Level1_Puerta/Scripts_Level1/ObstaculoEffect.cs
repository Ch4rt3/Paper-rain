using UnityEngine;
using System.Collections;

public class ObstaculoEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool yaEnSuelo = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Iniciar el desvanecimiento
        if (collision.gameObject.CompareTag("Suelo") && !yaEnSuelo)
        {
            yaEnSuelo = true;
            StartCoroutine(DesvanecerYDestruir());
        }
    }

    private IEnumerator DesvanecerYDestruir()
    {
        // Espera 1 segundos 
        yield return new WaitForSeconds(1f);

        float tiempoDesvanecimiento = 1f; 
        float tiempoTranscurrido = 0f;
        Color colorOriginal = spriteRenderer.color;

        while (tiempoTranscurrido < tiempoDesvanecimiento)
        {
            tiempoTranscurrido += Time.deltaTime;
            
            float nuevoAlfa = Mathf.Lerp(1f, 0f, tiempoTranscurrido / tiempoDesvanecimiento);
            spriteRenderer.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, nuevoAlfa);
            yield return null;
        }

        
        Destroy(gameObject);
    }
}