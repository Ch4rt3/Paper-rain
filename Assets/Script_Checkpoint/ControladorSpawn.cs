using UnityEngine;

public class ControladorSpawn : MonoBehaviour
{
    private bool yaReubicado = false;

    void LateUpdate()
    {
        if (!yaReubicado && DataHolder.instance != null && DataHolder.instance.tieneCheckpoint)
        {
            // Mueve el Transform principal
            transform.position = DataHolder.instance.posicionCheckpoint;

            
            // Lo buscamos el objeto
            Rigidbody2D rb = GetComponent<Rigidbody2D>() ?? GetComponentInChildren<Rigidbody2D>();
            
            if (rb != null)
            {
                rb.position = DataHolder.instance.posicionCheckpoint; 
                rb.linearVelocity = Vector2.zero; 
                rb.angularVelocity = 0f; 
            }

            yaReubicado = true;
            Debug.Log("¡Físicas y posición forzadas en el Checkpoint con éxito!");
        }
    }
}