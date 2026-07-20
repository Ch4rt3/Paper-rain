using UnityEngine;

public class ObjetoCheckpoint : MonoBehaviour
{
    [SerializeField] private ParticleSystem checkpointStars;

    private bool activado = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activado) return;

        Debug.Log("¡Trigger detectado! Tocado por: " + other.name);

        if (other.CompareTag("Player") ||
            (other.transform.parent != null && other.transform.parent.CompareTag("Player")))
        {
            activado = true;

            AudioManager.Instance.PlaySFX("checkpoint");

            DataHolder.instance.posicionCheckpoint = transform.position;
            DataHolder.instance.tieneCheckpoint = true;

            checkpointStars.Play();

            Debug.Log("¡Progreso guardado en el Checkpoint!");
        }
    }
}