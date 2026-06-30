using System.Collections; // 🌟 Obligatorio para poder usar Corrutinas (IEnumerator)
using UnityEngine;

public class ActivarTrampa : MonoBehaviour
{
    
    public Rigidbody2D[] listaPiñas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡Trampa activada! Iniciando caída en cadena...");
            
            
            StartCoroutine(CaerEnCadena());
            
            
            GetComponent<Collider2D>().enabled = false;
        }
    }

    
    private IEnumerator CaerEnCadena()
    {
        
        foreach (Rigidbody2D piña in listaPiñas)
        {
            if (piña != null)
            {
                piña.gravityScale = 3f; 
                Debug.Log("Cayó una piña.");
            }

            
            yield return new WaitForSeconds(0.5f);
        }
    }
}