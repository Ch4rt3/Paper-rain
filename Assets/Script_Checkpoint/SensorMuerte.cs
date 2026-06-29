using UnityEngine;
using UnityEngine.SceneManagement;

public class SensorMuerte : MonoBehaviour
{
     
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") || (collision.transform.parent != null && collision.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡La manzana aplastó a la moto!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("La manzana tocó el suelo y desapareció.");
            Destroy(gameObject); 
        }
    }
}