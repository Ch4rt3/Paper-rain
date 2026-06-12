using UnityEngine;
using System.Collections; 

public class Manija : MonoBehaviour
{
    [SerializeField] private GameObject objetoPuerta; 
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; 
    private bool yaGolpeada = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstaculo") && !yaGolpeada)
        {
            yaGolpeada = true;
            rb.bodyType = RigidbodyType2D.Dynamic;

            if (objetoPuerta != null)
            {
                Puerta scriptPuerta = objetoPuerta.GetComponent<Puerta>();
                if (scriptPuerta != null)
                {
                    scriptPuerta.DesaparecerPuerta();
                }
            }

            // Desvanecimiento de la manija
            StartCoroutine(DesvanecerManija());
        }
    }

    private IEnumerator DesvanecerManija()
    {
        // Esperar 1 segundos
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