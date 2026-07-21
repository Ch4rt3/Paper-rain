using UnityEngine;
using UnityEngine.SceneManagement;

public class AplastamientoTubo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D contacto in collision.contacts)
            {
                // Invertimos la condición: Si la normal apunta hacia ARRIBA desde el punto de vista del punto de choque,
                // significa que la cara inferior del tubo está impactando la parte superior del jugador.
                if (contacto.normal.y > 0.5f)
                {
                    Debug.Log("¡El tubo aplastó al jugador desde arriba!");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    return;
                }
            }

            Debug.Log("El jugador pasa por encima del tubo de forma segura.");
        }
    }
}