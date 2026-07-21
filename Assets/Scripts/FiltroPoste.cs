using UnityEngine;

public class FiltroPoste : MonoBehaviour
{
    [Header("Colliders del Poste")]
    [Tooltip("Arrastra aquí el PolygonCollider2D (la forma de la rampa)")]
    public Collider2D colliderRampa; 
    
    [Tooltip("Arrastra aquí el BoxCollider2D (la pared vertical para el Bulldozer)")]
    public Collider2D colliderParedBloqueo; 

    private Collider2D[] playerColliders;
    private PlayerTransform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // OBTENEMOS TODOS LOS COLLIDERS DEL JUGADOR (incluyendo llantas u objetos hijos)
            playerColliders = player.GetComponentsInChildren<Collider2D>();
            playerTransform = player.GetComponent<PlayerTransform>();
        }
    }

    void Update()
    {
        if (playerTransform != null && playerColliders != null && playerColliders.Length > 0)
        {
            bool isBulldozer = (playerTransform.currentForm == PlayerTransform.Form.Bulldozer);

            // Recorremos todos los colliders del jugador (ruedas, chasis, etc.)
            foreach (Collider2D pCollider in playerColliders)
            {
                if (pCollider != null)
                {
                    if (isBulldozer)
                    {
                        // Bulldozer: ignora la rampa, choca con la pared
                        if (colliderRampa != null) Physics2D.IgnoreCollision(colliderRampa, pCollider, true);
                        if (colliderParedBloqueo != null) Physics2D.IgnoreCollision(colliderParedBloqueo, pCollider, false);
                    }
                    else
                    {
                        // Moto/Avión: choca con la rampa para subir, ignora la pared
                        if (colliderRampa != null) Physics2D.IgnoreCollision(colliderRampa, pCollider, false);
                        if (colliderParedBloqueo != null) Physics2D.IgnoreCollision(colliderParedBloqueo, pCollider, true);
                    }
                }
            }
        }
    }
}
