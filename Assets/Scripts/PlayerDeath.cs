using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Transform _checkpoint;
    Rigidbody2D _rb; 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); 

        GameObject checkpointObj = GameObject.FindGameObjectWithTag(Names.TAG_CHECKPOINT);
        if (checkpointObj != null)
        {
            _checkpoint = checkpointObj.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Names.TAG_DEADZONE) && _checkpoint != null)
        {
            Respawn();
        }
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Names.TAG_ENEMY) && _checkpoint != null)
        {
            Respawn();
        }
    }*/

    
    void Respawn()
    {
        // 1. Teletransportamos
        transform.localPosition = _checkpoint.localPosition;

        // 2. SOLUCIÓN AL CHOQUE 1: Frenamos el cuerpo por completo
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero; // Resetea velocidad en X e Y
            _rb.angularVelocity = 0f;          // Resetea rotación física
        }

        // 3. SOLUCIÓN AL CHOQUE 2: Lo enderezamos por si venía de una rampa
        transform.rotation = Quaternion.identity;
    }
}