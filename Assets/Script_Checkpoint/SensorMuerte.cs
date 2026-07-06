using UnityEngine;
using UnityEngine.SceneManagement;

public class SensorMuerte : MonoBehaviour
{
     
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            TriggerDeath(other.gameObject);
        }
    }

     
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") || (collision.transform.parent != null && collision.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡La manzana aplastó a la moto!");
            TriggerDeath(collision.gameObject);
        }
        
        
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("La manzana tocó el suelo y desapareció.");
            Destroy(gameObject); 
        }
    }

    private void TriggerDeath(GameObject playerObj)
    {
        PlayerDeath playerDeath = playerObj.GetComponent<PlayerDeath>();
        if (playerDeath == null && playerObj.transform.parent != null)
        {
            playerDeath = playerObj.transform.parent.GetComponent<PlayerDeath>();
        }

        if (playerDeath != null)
        {
            playerDeath.Die();
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}