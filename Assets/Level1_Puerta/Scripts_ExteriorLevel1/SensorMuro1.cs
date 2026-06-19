using UnityEngine;

// Cambiamos el nombre de la clase aquí:
public class SensorMuro1 : MonoBehaviour
{
    private TextSalida scriptTexto;

    void Start()
    {
        scriptTexto = FindObjectOfType<TextSalida>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (scriptTexto != null)
            {
                scriptTexto.MostrarTextoSuave(); 
            }
        }
    }
}