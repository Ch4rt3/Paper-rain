using UnityEngine;

public class SensorBox : MonoBehaviour
{
    private TextBox scriptTextoBox;

    void Start()
    {
        // Busca el componente TextBox en la escena
        scriptTextoBox = FindObjectOfType<TextBox>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificamos que lo que entre al sensor sea la caja móvil
        if (other.CompareTag("Caja")) 
        {
            if (scriptTextoBox != null)
            {
                scriptTextoBox.ActivarTextoYPlataforma();
            }
        }
    }
}