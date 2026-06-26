using UnityEngine;
using UnityEngine.SceneManagement;

public class SensorMuerte : MonoBehaviour
{
    // zonas vacías de caída 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 2. Para la manzana cuando choca 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reinicia el nivel
        if (collision.gameObject.CompareTag("Player") || (collision.transform.parent != null && collision.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡La manzana aplastó a la moto!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        // La manzana se destruye sola y desaparece de la pantalla
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("La manzana tocó el suelo y desapareció.");
            Destroy(gameObject); // Destruye a la manzana
        }
    }
}