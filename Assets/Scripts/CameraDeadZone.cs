using UnityEngine;

public class CameraDeadZone : MonoBehaviour
{
    // Velocidad de la camara para seguir al jugador
    private Transform target;
    [SerializeField] float smoothSpeed = 5f;

    // Tamaño de la dead zone
    [SerializeField] float deadZoneX = 1.5f;
    [SerializeField] float deadZoneY = 0.8f;

    void LateUpdate()
    {
        
        if (target == null)
        {
            // Buscamos en la escena el objeto que tenga la etiqueta "Player"
            GameObject playerObject = GameObject.FindWithTag("Player");

            if (playerObject != null)
            {
                target = playerObject.transform;
            }
            else
            {
                // Si aún no nace el nuevo jugador en la escena, hacemos return
                return;
            }
        }

        
        float dx = target.position.x - transform.position.x;
        float dy = target.position.y - transform.position.y;

        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (Mathf.Abs(dx) > deadZoneX)
            targetX = target.position.x - Mathf.Sign(dx) * deadZoneX;

        if (Mathf.Abs(dy) > deadZoneY)
            targetY = target.position.y - Mathf.Sign(dy) * deadZoneY;

        Vector3 dest = new Vector3(targetX, targetY, transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position, dest,
            smoothSpeed * Time.deltaTime);
    }
}