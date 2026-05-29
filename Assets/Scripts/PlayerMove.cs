using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Configuración de Velocidades")]
    [SerializeField] float velocidadMoto = 8f;
    [SerializeField] float velocidadAvion = 5f;
    [SerializeField] float velocidadExcavadora = 2f;

    // Componentes del mismo Player
    Rigidbody2D _body;
    Animator _animator;
    //Componente PlayerTransform
    PlayerTransform _playerTransform; 

    // Acciones para el control
    InputAction _moveAction;

    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        // Buscamos el script PlayerTransform ubicado en el objeto Player
        _playerTransform = GetComponent<PlayerTransform>();

        _moveAction = InputSystem.actions["Player/Move"];
    }

    void Update()
    {
        
        // Si el jugador se está transformando, congelamos la velocidad en X y bloqueamos el movimiento
        if (_playerTransform != null && _playerTransform.isTransforming)
        {
            _body.linearVelocityX = 0;
            return; // Salimos del Update para que no lea más controles 
        }

        // 1. Obtener la velocidad correcta según el vehiculo actual
        float velocidadActual = ObtenerVelocidadPorForma();

        // 2. Fórmula del Movimiento
        Vector2 move = _moveAction.ReadValue<Vector2>();
        _body.linearVelocityX = move.x * velocidadActual;

        // Girar sprite
        if (move.x != 0)
        {
            if (move.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

        // Activar animación de movimiento
        if (_animator != null)
        {
            _animator.SetBool("IsMoving", move.x != 0);
        }
    }

    // Revisa qué forma tiene PlayerTransform y devuelve su velocidad
    float ObtenerVelocidadPorForma()
    {
        if (_playerTransform == null) return velocidadMoto; 

        switch (_playerTransform.currentForm)
        {
            case PlayerTransform.Form.Moto:
                return velocidadMoto;

            case PlayerTransform.Form.Excavadora:
                return velocidadExcavadora;

            case PlayerTransform.Form.Avion:
                return velocidadAvion;

            default:
                return velocidadMoto;
        }
    }
}