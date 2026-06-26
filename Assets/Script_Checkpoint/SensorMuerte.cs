using UnityEngine;
using UnityEngine.SceneManagement; 

public class SensorMuerte : MonoBehaviour
{
    // 1. Para las zonas vacías 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // 2. Para los obstáculos como manzana 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || (collision.transform.parent != null && collision.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡La manzana aplastó a la moto!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}