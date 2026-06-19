using UnityEngine;

public class SensorTriggerCentro : MonoBehaviour
{
    private TextHistory1 scriptTexto;

    void Start()
    {
        scriptTexto = FindObjectOfType<TextHistory1>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (scriptTexto != null)
            {
                scriptTexto.MostrarTextoSuave(); // Activa el Fade In de la chimenea
            }
        }
    }
}