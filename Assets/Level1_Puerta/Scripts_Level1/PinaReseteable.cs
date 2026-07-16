using UnityEngine;

public class PinaReseteable : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;
    private Rigidbody2D rb;

    void Awake()
    {
        // Guardamos dónde empezó la piña en el mapa al iniciar la partida
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
        rb = GetComponent<Rigidbody2D>();
    }

    // Llamaremos a esta función cada vez que el jugador muera y aparezca en el Checkpoint
    public void ResetearPiña()
    {
        // Devolvemos la piña a su sitio original
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;

        // Si la piña tiene física, detenemos su caída y velocidad acumulada
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // En Unity 6 se usa "linearVelocity" en lugar de "velocity"
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic; // La volvemos a poner estática hasta que se active
        }
    }
}