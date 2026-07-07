using System.Threading.Tasks;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Transform _checkpoint;
    Rigidbody2D _rb;
    Animator _animator;

    bool isDead = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        GameObject checkpointObj = GameObject.FindGameObjectWithTag(Names.TAG_CHECKPOINT);
        if (checkpointObj != null)
        {
            _checkpoint = checkpointObj.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        bool hasCheckpoint = (DataHolder.instance != null && DataHolder.instance.tieneCheckpoint) || _checkpoint != null;

        if (collision.CompareTag(Names.TAG_DEADZONE) && hasCheckpoint && !isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Detenemos el movimiento
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        // Desactivamos controles
        if (GetComponent<PlayerMove>() != null) GetComponent<PlayerMove>().enabled = false;
        if (GetComponent<PlayerTransform>() != null) GetComponent<PlayerTransform>().enabled = false;

        // Reproducimos la animación de muerte
        if (_animator != null) _animator.SetTrigger("Death");
        AudioManager.Instance.PlaySFX("muerte"); 
        // Esperamos que termine la animación
        Invoke(nameof(Respawn), 1f); // Cambia 1f por la duración de tu animación
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
        if (DataHolder.instance != null && DataHolder.instance.tieneCheckpoint)
        {
            transform.position = DataHolder.instance.posicionCheckpoint;
        }
        else if (_checkpoint != null)
        {
            transform.position = _checkpoint.position;
        }

        // 2. SOLUCIÓN AL CHOQUE 1: Frenamos el cuerpo por completo
        if (_rb != null)
        {
            _rb.linearVelocity = Vector2.zero; // Resetea velocidad en X e Y
            _rb.angularVelocity = 0f;          // Resetea rotación física
        }

        // 3. SOLUCIÓN AL CHOQUE 2: Lo enderezamos por si venía de una rampa
        transform.rotation = Quaternion.identity;

        // Reactivamos controles
        GetComponent<PlayerMove>().enabled = true;
        
        PlayerTransform pt = GetComponent<PlayerTransform>();
        if (pt != null)
        {
            pt.ForceResetToMoto();
            pt.enabled = true;
        }

        if (_animator != null)
        {
            _animator.Rebind();
            _animator.Update(0f);
        }

        isDead = false;
    }
}