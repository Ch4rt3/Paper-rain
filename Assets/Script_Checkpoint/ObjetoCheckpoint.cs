using UnityEngine;

public class ObjetoCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //se activa cuando la moto pasa por el sensor 
        Debug.Log("¡Trigger detectado! Tocado por: " + other.name);

        if (other.CompareTag("Player") || (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            DataHolder.instance.posicionCheckpoint = transform.position;
            DataHolder.instance.tieneCheckpoint = true;
            Debug.Log("¡Progreso guardado en el Checkpoint!");
        }
    }
}   