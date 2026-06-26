using System.Collections; // 🌟 Obligatorio para poder usar Corrutinas (IEnumerator)
using UnityEngine;

public class ActivarTrampa : MonoBehaviour
{
    // Arreglo para colocar tus 3 piñas en el Inspector
    public Rigidbody2D[] listaPiñas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Moto pasa por el sensor
        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            Debug.Log("¡Trampa activada! Iniciando caída en cadena...");
            
            // 🌟 Iniciamos la corrutina que controla el tiempo
            StartCoroutine(CaerEnCadena());
            
            // Desactivamos el colisionador del sensor para que no se vuelva a activar si la moto pasa de nuevo
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // ⏳ Esta función se encarga de pausar el tiempo entre cada piña
    private IEnumerator CaerEnCadena()
    {
        // Recorremos la lista de piñas una por una
        foreach (Rigidbody2D piña in listaPiñas)
        {
            if (piña != null)
            {
                piña.gravityScale = 2f; // Hace caer la piña actual
                Debug.Log("Cayó una piña.");
            }

            // ⏱️ ¡Aquí ocurre la magia! El script espera 2 segundos antes de pasar a la siguiente piña
            yield return new WaitForSeconds(1f);
        }
    }
}