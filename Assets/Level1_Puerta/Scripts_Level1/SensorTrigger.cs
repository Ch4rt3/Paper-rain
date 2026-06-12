using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbObstaculo;
    private bool yaPaso = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && !yaPaso)
        {
            yaPaso = true;
            rbObstaculo.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}