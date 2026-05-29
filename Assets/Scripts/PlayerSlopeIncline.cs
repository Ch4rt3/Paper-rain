using UnityEngine;

public class PlayerSlopeIncline : MonoBehaviour
{
    [Header("Configuración de Inclinación")]
    //Crear physic layer para el suelo y asignarlo en el inspector
    [SerializeField] private LayerMask conQueEsSuelo;
    // Largo del rayo que lanzamos hacia abajo para detectar el suelo.
    [SerializeField] private float largoDelRayo = 1.5f;
    [SerializeField] private float velocidadRotacion = 10f;
    // Componente PlayerTransform para verificar el vehiculo actual
    private PlayerTransform _playerTransform;

    void Awake()
    {
        _playerTransform = GetComponent<PlayerTransform>();
    }

    void Update()
    {
        // Si es un avion, probablemente no deba inclinarse con el suelo.
        // Podemos ignorar la lógica si está volando:
        if (_playerTransform != null && _playerTransform.currentForm == PlayerTransform.Form.Avion)
        {
            // Resetear rotación en el aire para el avión de forma suave
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, velocidadRotacion * Time.deltaTime);
            return;
        }

        AjustarInclinacionConSuelo();
    }

    void AjustarInclinacionConSuelo()
    {
        // Lanzamos un rayo invisible desde el centro del personaje hacia abajo
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, largoDelRayo, conQueEsSuelo);

        // Dibujamos el rayo en la pestaña de "Scene" para poder calibrarlo visualmente
        Debug.DrawRay(transform.position, Vector2.down * largoDelRayo, Color.red);

        if (hit.collider != null)
        {
            // 'hit.normal' nos da un vector perpendicular a la superficie del suelo.
            // Si el suelo es plano, el vector apunta recto hacia arriba (0, 1).
            // Si es una rampa, el vector se inclina.
            Vector2 normalDelSuelo = hit.normal;

            // Calculamos el ángulo en grados basándonos en la inclinación del suelo
            float anguloInclinacion = Mathf.Atan2(normalDelSuelo.x, normalDelSuelo.y) * Mathf.Rad2Deg;

            // Creamos la rotación en el eje Z (multiplicamos por -1 para que incline hacia el lado correcto)
            Quaternion rotacionObjetivo = Quaternion.Euler(0, 0, -anguloInclinacion);

            // Aplicamos la rotación de forma suave (Lerp) para que no sea un golpe brusco
            transform.rotation = Quaternion.Lerp(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
        }
        else
        {
            // Si el personaje salta o está en el aire, vuelve a ponerse recto (rotación 0)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, velocidadRotacion * Time.deltaTime);
        }
    }
}