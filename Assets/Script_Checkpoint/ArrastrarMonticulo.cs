using UnityEngine;

public class ArrastrarMonticulo : MonoBehaviour
{
    [Header("Efecto de Tierra")]
    public ParticleSystem particulasTierra;

    [Header("Configuración de Sensibilidad")]
    [Tooltip("Distancia mínima recorrida por frame para considerar que se está moviendo")]
    public float sensibilidadMovimiento = 0.001f;

    private Vector3 ultimaPosicion;

    void Start()
    {
        // Guardamos la posición inicial
        ultimaPosicion = transform.position;

        if (particulasTierra != null)
        {
            // Nos aseguramos de que empiece apagado sin importar el inspector
            particulasTierra.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void Update()
    {
        if (particulasTierra == null) return;

        // Calculamos la distancia que se ha movido el objeto desde el frame anterior
        float distanciaRecorrida = Vector3.Distance(transform.position, ultimaPosicion);

        // Si el objeto cambió de posición en el espacio (se está moviendo)
        if (distanciaRecorrida > sensibilidadMovimiento)
        {
            if (!particulasTierra.isPlaying)
            {
                particulasTierra.Play();
            }
        }
        else // Si el objeto se quedó quieto
        {
            if (particulasTierra.isPlaying)
            {
                particulasTierra.Stop();
            }
        }

        // Actualizamos la posición para el siguiente frame
        ultimaPosicion = transform.position;
    }
}